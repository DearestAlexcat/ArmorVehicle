using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;
using Zenject;

namespace ArmorVehicle
{
    [RequireComponent(typeof(Rigidbody))]
    public class Projectile : MonoBehaviour, IPoolReturnable<Projectile>
    {
        public event Action<Projectile> OnReturned;

        [SerializeField] TrailRenderer trailRenderer;

        Vector3 flyDirection;
        ProjectileConfig config;
        Rigidbody thisRigidbody;
        CancellationTokenSource cts;

        void Start()
        {
            thisRigidbody = GetComponent<Rigidbody>();
        }

        [Inject]
        public void Construct(IStaticDataService staticDataService)
        {
            config = staticDataService.ProjectileConfig;
        }

        public void ReturnToPool()
        {
            OnReturned?.Invoke(this);
        }

        public void Initialize(Transform firePoint)
        {
            transform.position = firePoint.position;
            trailRenderer.Clear();
            flyDirection = firePoint.forward;
            transform.rotation = Quaternion.LookRotation(flyDirection);

            if (cts != null)
            {
                cts.Cancel();
                cts.Dispose();
                cts = null;
            }

            cts = new CancellationTokenSource();
            FlyAndReturnAsync(cts.Token).Forget();
        }

        async UniTask FlyAndReturnAsync(CancellationToken cancellationToken)
        {
            try
            {
                await UniTask.Delay(TimeSpan.FromSeconds(config.flyTime), cancellationToken: cancellationToken);
                ReturnToPool();
            }
            catch (OperationCanceledException)
            {
            }
        }

        void FixedUpdate()
        {
            thisRigidbody.MovePosition(thisRigidbody.position + flyDirection * config.flySpeed * Time.fixedDeltaTime);
        }

        public void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.CompareTag(GameConstants.EnemyTag)) return;

            cts.Cancel();
            ReturnToPool();

            other.gameObject.GetComponent<IHealth>().TakeDamage(config.projectileDamage, transform.forward);
        }
    }
}