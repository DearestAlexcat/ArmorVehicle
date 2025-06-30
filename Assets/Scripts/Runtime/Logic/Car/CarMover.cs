using Zenject;

namespace ArmorVehicle
{
    public class CarMover
    {
        readonly IInputService inputService;

        TurretShooting turretShooting;
        TurretRotator turretRotator;
        CarForwardMovement forwardMovement;
        
        [Inject]
        public CarMover(IInputService inputService, Car car, IStaticDataService staticDataService, IInteractorPoolContainer poolContainer, IHudView hudView)
        {
            this.inputService = inputService;

            forwardMovement = new CarForwardMovement(car, staticDataService.CarConfig);
            turretRotator = new TurretRotator(car, inputService, staticDataService);
            turretShooting = new TurretShooting(car, inputService, staticDataService.CarConfig, poolContainer);
        }

        public void Subscribe()
        {
            turretRotator.Subscribe();
            turretShooting.Subscribe();
        }

        public void Unsubscribe()
        {
            turretRotator.Unsubscribe();
            turretShooting.Unsubscribe();
        }

        public void Update()
        {
            forwardMovement.UpdatePosition();
            turretShooting.ShotUpdate();
        }
    }
}