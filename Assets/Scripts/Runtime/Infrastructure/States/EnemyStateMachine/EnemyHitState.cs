using Cysharp.Threading.Tasks;

namespace ArmorVehicle
{
    public class EnemyHitState : IState
    {
        readonly EnemyStateMachine stateMachine;
        readonly EnemyAnimator animator;
        readonly Enemy enemy;

        public EnemyHitState(EnemyStateMachine stateMachine, EnemyAnimator animator, Enemy enemy)
        {
            this.stateMachine = stateMachine;
            this.animator = animator;
            this.enemy = enemy;
        }

        public UniTask Enter()
        {
            switch (enemy.GetState())
            {
                case EnemyState.Idle:
                    animator.PlayHit(() => stateMachine.Enter<EnemyIdleState, EnemyState>(EnemyState.Idle).Forget());
                    break;
                case EnemyState.Pursuit:           
                    animator.PlayHit(() => stateMachine.Enter<EnemyPursuitState>().Forget());
                    break;
                case EnemyState.Wander:
                    animator.PlayHit(() => stateMachine.Enter<EnemyWanderState>().Forget());
                    break;
            }

            return default;
        }

        public UniTask Exit()
        {
            return default;
        }
    }
}