using Cysharp.Threading.Tasks;
using Zenject;

namespace ArmorVehicle
{
    public class GameLoopState : IState, IUpdatebleState
    {
        readonly CarStateMachine carStateMachine;
        readonly IHudView hudView;
        readonly MovementTracker movementTracker;

        [Inject]
        public GameLoopState(CarStateMachine carStateMachine, IHudView hudView, IStaticDataService staticDataService, MovementTracker movementTracker)
        {
            this.carStateMachine = carStateMachine;
            this.hudView = hudView;
            this.movementTracker = movementTracker;

            movementTracker.Initialize(staticDataService.CarConfig.moveSpeed);
        }

        public UniTask Enter()
        {
            hudView.Display(true);

            movementTracker.Start();

            carStateMachine.Enter<CarRunState>().Forget();
         
            return default;
        }

        public UniTask Exit()
        {
            hudView.Display(false);
            return default;
        }

        public void Update()
        {
            hudView.UpdateHUD(movementTracker);
        }
    }
}