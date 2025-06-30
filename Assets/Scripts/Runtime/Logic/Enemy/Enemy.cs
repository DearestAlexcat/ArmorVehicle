using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using Zenject;

namespace ArmorVehicle
{
    public class Enemy : MonoBehaviour, IPoolReturnable<Enemy>
    {
        [SerializeField] EnemyAnimator animator;
        
        EnemyState enemyState;
        public event Action<Enemy> OnReturned;

        Car car;
        IHealth health;
        EnemyConfig config;
        EnemyStateMachine enemyStateMachine;
        bool hasFirstSpawnOccurred;

        public Vector3 spawnZonePosition;
        public float spawnZoneRadius;

        void Start()
        {
            hasFirstSpawnOccurred = true;
            IdleStateTransition();
        }

        void OnEnable()
        {
            health.Current = health.Max;

            if(hasFirstSpawnOccurred)
                IdleStateTransition();
        }

        void IdleStateTransition()
        {
            enemyStateMachine.Enter<EnemyIdleState, EnemyState>(EnemyState.Idle).Forget();
        }

        public EnemyState GetState()
        {
            return enemyState;
        }

        public void SetState(EnemyState state)
        {
            enemyState = state;
        }

        [Inject]
        public void Construct(Car car, IStaticDataService staticDataService, EnemyStateMachine enemyStateMachine)
        {
            this.car = car;
            this.enemyStateMachine = enemyStateMachine;

            config = staticDataService.EnemyConfig;

            health = GetComponent<IHealth>();
            if (health != null)
                health.Current = health.Max = config.hp;
            else
                Debug.LogError("Health component not found");
        }

        public int GetDamage()
        {
            return config.damage;
        }

        public void ReturnToPool()
        {
            OnReturned?.Invoke(this);
        }

        void Update()
        {
            enemyStateMachine.UpdateState();
        }
    }
}