using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace ArmorVehicle
{
    public class EnemyHealth : Health
    {
        [SerializeField] DamageFlashEffect flashEffect;
        [SerializeField] EnemyFx enemyFx;

        EnemyStateMachine stateMachine;

        [Inject]
        void Construct(EnemyStateMachine stateMachine, Enemy enemy, IStaticDataService staticDataService)
        {
            this.stateMachine = stateMachine;
        }

        public override void TakeDamage(int damage, Vector3 direction)
        {
            base.TakeDamage(damage);

            flashEffect.FlashWhite();
            
            enemyFx.PlayBloodDebrisFX(direction);

            stateMachine.Enter<EnemyHitState>().Forget();
        }
    }
}