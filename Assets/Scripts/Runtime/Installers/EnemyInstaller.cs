using Zenject;

namespace ArmorVehicle
{
    public class EnemyInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindEnemyStateMachine();
            BindStatesFactory();
            BindEnemy();
            BindEnemyAnimator();
        }

        void BindEnemyStateMachine()
        {
            Container.Bind<EnemyStateMachine>().AsSingle();

            Container.Bind<EnemyIdleState>().AsSingle();
            Container.Bind<EnemyHitState>().AsSingle();
            Container.Bind<EnemyPursuitState>().AsSingle();
            Container.Bind<EnemyWanderState>().AsSingle();
            Container.Bind<EnemyDeadState>().AsSingle();
        }

        void BindStatesFactory()
        {
            Container.BindInterfacesTo<StatesFactory>().AsSingle();
        }

        void BindEnemy()
        {
            Container.Bind<Enemy>().FromComponentOnRoot().AsSingle();
            Container.Bind<EnemyPursuitTransitionChecker>().AsSingle();
        }

        void BindEnemyAnimator()
        {
            Container.Bind<EnemyAnimator>().FromComponentOnRoot().AsSingle();
        }
    }
}