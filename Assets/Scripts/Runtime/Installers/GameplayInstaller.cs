using UnityEngine;
using Zenject;

namespace ArmorVehicle
{
    public class GameplayInstaller : MonoInstaller
    {
        [SerializeField] MenuView menuView;
        [SerializeField] HUDView hudView;
        [SerializeField] Car car;

        public override void InstallBindings()
        {
            BindGameplayStateMachine();
            BindCarStateMachine();

            BindStatesFactory();

            BindGameFactory();

            BindServices();
            BindPoolServices();

            BindPlacers();
            BindView();
            BindCar();
            BindMovementTracker();
        }

        void BindMovementTracker()
        {
            Container.Bind<MovementTracker>().AsSingle();
        }

        void BindCar()
        {
            Container.BindInterfacesAndSelfTo<Car>().FromInstance(car).AsSingle();
        }

        void BindCarStateMachine()
        {
            Container.Bind<CarStateMachine>().AsSingle();
        }

        void BindView()
        {
            Container.Bind<IMenuView>().FromInstance(menuView).AsSingle();
            Container.Bind<IHudView>().FromInstance(hudView).AsSingle();
        }

        void BindGameplayStateMachine()
        {
            Container.Bind<GameplayStateMachine>().AsSingle();

            Container.Bind<GameplayBootstrapState>().AsSingle();
            Container.Bind<InitGameplayState>().AsSingle();
            Container.Bind<MenuState>().AsSingle();
            Container.Bind<GameLoopState>().AsSingle();
            Container.Bind<WinGameState>().AsSingle();
            Container.Bind<LoseGameState>().AsSingle();
        }

        void BindServices()
        {
            Container.BindInterfacesTo<StaticDataService>().AsSingle();
        }

        void BindPlacers()
        {
            Container.BindInterfacesAndSelfTo<ChunkPlacer>().AsSingle();
            Container.BindInterfacesAndSelfTo<EnemyPlacer>().AsSingle();
        }

        void BindStatesFactory()
        {
            Container.BindInterfacesTo<StatesFactory>().AsSingle();
        }

        void BindGameFactory()
        {
            Container.BindInterfacesTo<GameFactory>().AsSingle();
        }

        void BindPoolServices()
        {
            Container.BindInterfacesTo<PoolObjectsContainer>().AsSingle();
            Container.BindInterfacesTo<InteractorPoolContainer>().AsSingle();
        }
    }
}