using Cysharp.Threading.Tasks;
using Zenject;

namespace ArmorVehicle
{
    public class WinGameState : IState
    {
        IGameFactory gameFactory;

        [Inject]
        void Construct(GameplayStateMachine gameplayStateMachine, IGameFactory gameFactory)
        {
            this.gameFactory = gameFactory;
        }

        public UniTask Enter()
        {
            gameFactory.CreateWinWindow();
            return default;
        }

        public UniTask Exit()
        {
            return default;
        }
    }
}