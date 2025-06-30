using Cysharp.Threading.Tasks;

namespace ArmorVehicle
{
    public class CarIdleState : IState
    {
        Car car;
        CameraFollow cameraFollow;

        public CarIdleState(Car car, CameraFollow cameraFollow)
        {
            this.car = car;
            this.cameraFollow = cameraFollow;
        }

        public UniTask Enter()
        {
            cameraFollow.enabled = false;
            cameraFollow.SetOrigin(car.CameraOrigin);
            return default;
        }

        public UniTask Exit()
        {
            car.DisplayComponents(true);

            cameraFollow.enabled = true;

            return default;
        }
    }
}
