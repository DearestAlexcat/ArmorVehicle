using Cysharp.Threading.Tasks;
using Zenject;

namespace ArmorVehicle
{
    public class CarDeadState : IState
    {
        GameplayStateMachine gameplayStateMachine;

        [Inject]
        void Construct(GameplayStateMachine gameplayStateMachine, CameraShake cameraShake)
        {
            this.gameplayStateMachine = gameplayStateMachine;
        }

        public UniTask Enter()
        {
            gameplayStateMachine.Enter<LoseGameState>().Forget();
            return default;
        }

        public UniTask Exit()
        {
            return default;
        }
    }
}
