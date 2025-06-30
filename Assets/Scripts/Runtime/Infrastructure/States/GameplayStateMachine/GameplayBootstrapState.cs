using Cysharp.Threading.Tasks;
using Zenject;

namespace ArmorVehicle
{
    public class GameplayBootstrapState : IState
    {
        readonly GameplayStateMachine gameplayStateMachine;

        readonly IAssetProvider assetProvider;
        readonly IStaticDataService staticDataService;
        readonly IGameFactory gameFactory;
        readonly ILogService logService;

        [Inject]
        public GameplayBootstrapState(GameplayStateMachine gameplayStateMachine, IAssetProvider assetProvider, ILogService logService, 
                                IStaticDataService staticDataService, IGameFactory gameFactory)
        {
            this.gameplayStateMachine = gameplayStateMachine;
            this.assetProvider = assetProvider;
            this.logService = logService;
            this.staticDataService = staticDataService;
            this.gameFactory = gameFactory;
        }

        public async UniTask Enter()
        {
            logService.Log("Entered GameplayBootstrapState");

            await assetProvider.PreloadAssetByLabel(AssetPath.LevelLabel);
            await staticDataService.InitializeAsync();
            await gameFactory.InitializeAsync();

            gameplayStateMachine.Enter<InitGameplayState>().Forget();
        }

        public UniTask Exit()
        {
            return default;
        }
    }
}