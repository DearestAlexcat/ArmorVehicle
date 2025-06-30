using Cysharp.Threading.Tasks;
using UnityEngine;

namespace ArmorVehicle
{
    public class EnemyPursuitState : IState, IUpdatebleState
    {
        readonly EnemyStateMachine stateMachine;
        readonly EnemyAnimator animator;
        readonly IStaticDataService staticDataService;
        readonly Car car;
        readonly Enemy enemy;

        EnemyConfig config;

        public EnemyPursuitState(EnemyStateMachine stateMachine, EnemyAnimator animator, IStaticDataService staticDataService, Car car, Enemy enemy)
        {
            this.stateMachine = stateMachine;
            this.animator = animator;
            config = staticDataService.EnemyConfig;
            this.car = car;
            this.enemy = enemy;
        }

        public UniTask Enter()
        {
            animator.PlayMove(true);
            return default;
        }

        public UniTask Exit()
        {
            animator.PlayMove(false);
            return default;
        }

        public void Update()
        {
            CheckCarAlive();
            MoveTowardsPlayer();
            
            if (Vector3.Distance(enemy.transform.position, car.transform.position) > config.triggerDistance)
            {
                enemy.SetState(EnemyState.Idle);
                stateMachine.Enter<EnemyIdleState, EnemyState>(EnemyState.Idle).Forget();
            }
        }

        void CheckCarAlive()
        {
            if(!car.IsAlive)
                stateMachine.Enter<EnemyIdleState, EnemyState>(EnemyState.Stop).Forget();
        }

        void MoveTowardsPlayer()
        {
            Vector3 currentPosition = enemy.transform.position;
            Vector3 targetPosition = car.transform.position;

            enemy.transform.position = Vector3.MoveTowards(
                currentPosition,
                targetPosition,
                config.speed * Time.deltaTime
            );

            enemy.transform.LookAt(targetPosition);
        }
    }
}