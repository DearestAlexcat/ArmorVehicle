using Cysharp.Threading.Tasks;
using UnityEngine;

namespace ArmorVehicle
{
    public class EnemyPursuitTransitionChecker
    {
        readonly Car car;
        readonly Enemy enemy;
        readonly EnemyStateMachine stateMachine;
        readonly EnemyConfig config;

        public EnemyPursuitTransitionChecker(EnemyStateMachine stateMachine, Car car, Enemy enemy, IStaticDataService staticDataService)
        {
            this.stateMachine = stateMachine;
            this.car = car;
            this.enemy = enemy;
            config = staticDataService.EnemyConfig;
        }

        public void Update()
        {
            if (Vector3.Distance(enemy.transform.position, car.transform.position) <= config.triggerDistance)
            {
                enemy.SetState(EnemyState.Pursuit);
                stateMachine.Enter<EnemyPursuitState>().Forget();
            }
        }
    }
}