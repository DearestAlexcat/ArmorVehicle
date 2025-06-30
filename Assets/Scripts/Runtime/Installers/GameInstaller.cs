using Zenject;

namespace ArmorVehicle
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindGameStateMachine();
            BindStatesFactory();
            BindAssetProvider();
            BindServices();
        }

        void BindGameStateMachine()
        {
            Container.Bind<GameStateMachine>().AsSingle();

            Container.Bind<BootstrapState>().AsSingle();
            Container.Bind<LoadSceneState>().AsSingle();
        }

        void BindStatesFactory()
        {
            Container.BindInterfacesTo<StatesFactory>().AsSingle();
        }

        void BindAssetProvider()
        {
            Container.BindInterfacesTo<AssetProvider>().AsSingle();
        }

        void BindServices()
        {
            Container.BindInterfacesTo<SceneLoader>().AsSingle();
            Container.BindInterfacesTo<LogService>().AsSingle();
        }
    }
}