using Cysharp.Threading.Tasks;
using Zenject;

namespace ArmorVehicle
{
    public class CarRunState : IState, IUpdatebleState
    {
        readonly CarMover carMover;
        readonly CameraShake cameraShake;

        [Inject]
        public CarRunState(CarMover carMover, CameraShake cameraShake)
        {
            this.carMover = carMover;
            this.cameraShake = cameraShake;
        }

        public UniTask Enter()
        {
            cameraShake.StartShake();
            carMover.Subscribe();
            return default;
        }

        public void Update()
        {
            carMover.Update();
        }

        public UniTask Exit()
        {
            cameraShake.StopShake();
            carMover.Unsubscribe();
            return default;
        }
    }
}
