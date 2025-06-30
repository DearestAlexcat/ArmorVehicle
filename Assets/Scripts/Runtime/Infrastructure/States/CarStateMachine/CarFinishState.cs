using Cysharp.Threading.Tasks;
using Zenject;

namespace ArmorVehicle
{
    public class CarFinishState : IState
    {
        GameplayStateMachine gameplayStateMachine;
        
        [Inject]
        void Construct(GameplayStateMachine gameplayStateMachine)
        {
            this.gameplayStateMachine = gameplayStateMachine;
        }

        public UniTask Enter()
        {
            gameplayStateMachine.Enter<WinGameState>().Forget();
            return default;
        }

        public UniTask Exit()
        {
            return default;
        }
    }
}