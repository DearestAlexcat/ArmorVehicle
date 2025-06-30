using UnityEngine;
using Zenject;

namespace ArmorVehicle
{
    public class EnemyDeath : MonoBehaviour
    {
        [SerializeField] Enemy enemy;
        [SerializeField] Health health;
        [SerializeField] EnemyFx enemyFx;

        EnemyPlacer enemyPlacer;

        [Inject]
        void Construct(EnemyPlacer enemyPlacer)
        {
            this.enemyPlacer = enemyPlacer;
        }

        void Start()
        {
            health.HealthChanged += OnHealthChanged;
        }

        void OnDestroy()
        {
            health.HealthChanged -= OnHealthChanged;
        }

        void OnHealthChanged()
        {
            if (health.Current <= 0)
                Die();
        }

        public void Die()
        {
            enemyFx.PlayBloodDebrisFX(-transform.forward);
            enemyPlacer.DestroyEnemy(enemy);
        }
    }
}