using UnityEngine;
using Zenject;

namespace ArmorVehicle
{
    public class CarInstaller : MonoInstaller
    {
        [SerializeField] CameraFollow cameraFollow;
        [SerializeField] CameraShake cameraShake;

        public override void InstallBindings()
        {
            BindCarStateMachine();
            BindStatesFactory();
            BindCar();
            BindCamera();
            BindInputService();
            BindCameraShake();
        }

        void BindCarStateMachine()
        {
            Container.Bind<CarIdleState>().AsSingle();
            Container.Bind<CarRunState>().AsSingle();
            Container.Bind<CarFinishState>().AsSingle();
            Container.Bind<CarDeadState>().AsSingle();
        }

        void BindCameraShake()
        {
            Container.Bind<CameraShake>().FromInstance(cameraShake).AsSingle();
        }

        private void BindInputService()
        {
            Container.BindInterfacesAndSelfTo<MobileInputService>().AsSingle();
        }

        void BindStatesFactory()
        {
            Container.BindInterfacesTo<StatesFactory>().AsSingle();
        }

        void BindCamera()
        {
            Container.Bind<CameraFollow>().FromInstance(cameraFollow).AsSingle();
        }

        void BindCar()
        {
            Container.Bind<CarMover>().AsSingle();
        }
    }
}
