using Cysharp.Threading.Tasks;
using Zenject;

namespace ArmorVehicle
{
    public class MenuState : IState
    {
        readonly IMenuView menuView;
        readonly ILogService logService;
        readonly GameplayStateMachine gameplayStateMachine;

        [Inject]
        public MenuState(ILogService logService, GameplayStateMachine gameplayStateMachine, IMenuView menuView)
        {
            this.menuView = menuView;
            this.logService = logService;
            this.gameplayStateMachine = gameplayStateMachine;
        }

        public UniTask Enter()
        {
            logService.Log("Entered MenuState");

            menuView.Display(true);
            menuView.OnStartGame += StartGame;

            return default;
        }

        void StartGame()
        {
            gameplayStateMachine.Enter<GameLoopState>().Forget();
        }

        public UniTask Exit()
        {
            menuView.OnStartGame -= StartGame;
            menuView.Display(false);

            return default;
        }
    }
}