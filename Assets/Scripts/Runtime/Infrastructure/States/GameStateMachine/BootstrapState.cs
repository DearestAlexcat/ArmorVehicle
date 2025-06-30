using Cysharp.Threading.Tasks;
using Zenject;

namespace ArmorVehicle
{
    public class BootstrapState : IState
    {
        readonly GameStateMachine gameStateMachine;

        readonly IAssetProvider assetProvider;
        readonly ILogService logService;

        [Inject]
        public BootstrapState(GameStateMachine gameStateMachine, IStatesFactory statesFactory, IAssetProvider assetProvider, ILogService logService)
        {
            this.gameStateMachine = gameStateMachine;
            this.assetProvider = assetProvider;
            this.logService = logService;
        }

        public async UniTask Enter()
        {
            logService.Log("Entered BootstrapState");

            await assetProvider.InitializeAsync();

            gameStateMachine.Enter<LoadSceneState, string>(SceneName.Level.ToString()).Forget();
        }

        public UniTask Exit()
        {
            return default;
        }
    }
}