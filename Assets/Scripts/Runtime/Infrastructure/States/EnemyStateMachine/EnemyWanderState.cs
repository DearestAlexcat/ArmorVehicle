using Cysharp.Threading.Tasks;
using UnityEngine;

namespace ArmorVehicle
{
    public class EnemyWanderState : IState, IUpdatebleState
    {
        readonly EnemyStateMachine stateMachine;
        readonly EnemyAnimator animator;
        readonly EnemyConfig config;
        readonly Enemy enemy;
        readonly Car car;
        readonly EnemyPursuitTransitionChecker pursuitTransitionChecker;

        Vector3 destenationPoint;

        public EnemyWanderState(EnemyStateMachine stateMachine, EnemyAnimator animator, IStaticDataService staticDataService, 
            Enemy enemy, EnemyPursuitTransitionChecker pursuitTransitionChecker, Car car)
        {
            this.stateMachine = stateMachine;
            this.animator = animator;
            config = staticDataService.EnemyConfig;
            this.enemy = enemy;
            this.pursuitTransitionChecker = pursuitTransitionChecker;
            this.car = car;
        }

        public UniTask Enter()
        {
            animator.PlayWalking(true);
            enemy.SetState(EnemyState.Wander);
            destenationPoint = GetRandomPointInCircle(enemy.spawnZonePosition, enemy.spawnZoneRadius);

            return default;
        }

        public UniTask Exit()
        {
            animator.PlayWalking(false);
            return default;
        }

        public void Update()
        {
            pursuitTransitionChecker.Update();

            CheckCarAlive();
            MoveTowardsPoint();

            if (Vector3.Distance(enemy.transform.position, destenationPoint) < 0.1f)
            {
                stateMachine.Enter<EnemyIdleState, EnemyState>(EnemyState.Idle).Forget();
            }
        }

        void CheckCarAlive()
        {
            if (!car.IsAlive)
                stateMachine.Enter<EnemyIdleState, EnemyState>(EnemyState.Stop).Forget();
        }

        Vector3 GetRandomPointInCircle(Vector3 center, float radius)
        {
            Vector2 randomPoint2D = Random.insideUnitCircle * radius;
            return center + new Vector3(randomPoint2D.x, 0f, randomPoint2D.y);
        }

        void MoveTowardsPoint()
        {
            Vector3 currentPosition = enemy.transform.position;
            Vector3 targetPosition = destenationPoint;

            enemy.transform.position = Vector3.MoveTowards(
                currentPosition,
                targetPosition,
                config.walkingSpeed * Time.deltaTime
            );

            enemy.transform.LookAt(targetPosition);
        }
    }
}