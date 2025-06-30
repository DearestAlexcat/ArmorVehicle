using Cysharp.Threading.Tasks;
using System.Threading;
using System;

namespace ArmorVehicle
{
    public class EnemyIdleState : IState<EnemyState>, IUpdatebleState
    {
        readonly EnemyStateMachine stateMachine;
        readonly EnemyAnimator animator;
        readonly Enemy enemy;
        readonly EnemyConfig config;
        readonly EnemyPursuitTransitionChecker pursuitTransitionChecker;
        
        CancellationTokenSource cts;

        public EnemyIdleState(EnemyStateMachine stateMachine, EnemyAnimator animator, 
            IStaticDataService staticDataService, Enemy enemy, EnemyPursuitTransitionChecker pursuitTransitionChecker)
        {
            this.stateMachine = stateMachine;
            this.animator = animator;
            config = staticDataService.EnemyConfig;
            this.pursuitTransitionChecker = pursuitTransitionChecker;
            this.enemy = enemy;
        }

        public UniTask Enter(EnemyState state)
        {
            animator.PlayIdle(true);

            enemy.SetState(state);

            cts = new CancellationTokenSource();
            WaitThenStateAsync(cts.Token).Forget();

            return default;
        }

        async UniTask WaitThenStateAsync(CancellationToken cancellationToken)
        {
            try
            {
                await UniTask.Delay(TimeSpan.FromSeconds(config.idleDuration), cancellationToken: cancellationToken);

                if (enemy.GetState() == EnemyState.Idle)
                {
                    stateMachine.Enter<EnemyWanderState>().Forget();
                }
            }
            catch (OperationCanceledException)
            {
            }
        }

        void ResetCancellationTokenSource()
        {
            if (cts != null)
            {
                cts.Cancel();
                cts.Dispose();
                cts = null;
            }
        }

        void OnDisable()
        {
            ResetCancellationTokenSource();
        }

        public UniTask Exit()
        {
            cts?.Cancel();
            cts?.Dispose();

            animator.PlayIdle(false);
            return default;
        }


        public void Update()
        {
            pursuitTransitionChecker.Update();
        }
    }
}