using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace ArmorVehicle
{
    [RequireComponent(typeof(Rigidbody))]
    public class Car : MonoBehaviour, ITickable
    {
        [SerializeField] Transform turret;
        [SerializeField] Transform cameraOrigin;
        [SerializeField] Transform firePoint;
        [SerializeField] GameObject laser;
        [SerializeField] Transform carMover;
        [SerializeField] ParticleSystem fireFx;
        [SerializeField] CarHealth health;

        [SerializeField] Animator turretFireAnimator;

        public bool IsAlive => health.Current > 0;
        public Transform Turret => turret;
        public ParticleSystem FireFx => fireFx;
        public Transform CarMover => carMover;
        public Transform FirePoint => firePoint;
        public Transform CameraOrigin => cameraOrigin;
        public Rigidbody Rigidbody { get; private set; }

        IStatesFactory statesFactory;
        CarStateMachine stateMachine;

        readonly int turretFireAnim = Animator.StringToHash("Fire");

        [Inject]
        void Construct(CarStateMachine stateMachine, IStatesFactory statesFactory)
        {
            this.statesFactory = statesFactory;
            this.stateMachine = stateMachine;
        }

        void Awake()
        {
            Rigidbody = GetComponent<Rigidbody>();
        }

        void Start()
        {
            DisplayComponents(false);

            stateMachine.SetStatesFactory(statesFactory);
            stateMachine.Enter<CarIdleState>().Forget();
        }

        public void PlayTurretFire()
        {
            fireFx.Play(true);
            turretFireAnimator.SetTrigger(turretFireAnim);
        }

        public void DisplayComponents(bool value)
        {
            laser.SetActive(value);
            gameObject.GetComponentInChildren<HealthPointsBar>()?.Display(value);
        }

        public void Tick()
        {
            stateMachine.UpdateState();
        }

        public void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag(GameConstants.EnemyTag))
            {
                var enemy = other.gameObject.GetComponent<Enemy>();

                gameObject.GetComponentInChildren<IHealth>().TakeDamage(enemy.GetDamage());

                other.gameObject.GetComponent<EnemyDeath>().Die();
            }

            if (other.gameObject.CompareTag(GameConstants.FinishTag))
            {
                stateMachine.Enter<CarFinishState>().Forget();
            }
        }
    }
}
