using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace ArmorVehicle
{
    public class CarDeath : MonoBehaviour
    {
        [SerializeField] Car car;
        [SerializeField] Health health;
        [SerializeField] GameObject deathFx;

        CarStateMachine carStateMachine;

        [Inject]
        void Construct(CarStateMachine carStateMachine)
        {
            this.carStateMachine = carStateMachine;
        }

        private void Start()
        {
            health.HealthChanged += OnHealthChanged;
        }

        private void OnDestroy()
        {
            health.HealthChanged -= OnHealthChanged;
        }

        private void OnHealthChanged()
        {
            if (health.Current <= 0)
                Die();
        }

        private void Die()
        {
            health.HealthChanged -= OnHealthChanged;

            //SpawnDeathFx();

            carStateMachine.Enter<CarDeadState>().Forget();
        }

        private void SpawnDeathFx()
        {
            Instantiate(deathFx, transform.position, Quaternion.identity);
        }
    }
}