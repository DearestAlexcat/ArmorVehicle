using UnityEngine;

namespace ArmorVehicle
{
    public class TurretShooting
    {
        readonly IInputService inputService;
        readonly Car car;
        readonly CarConfig carConfig;
        readonly IInteractorPoolContainer interactorPool;

        public bool isShooting;

        float timer;

        public TurretShooting(Car car, IInputService inputService, CarConfig carConfig, IInteractorPoolContainer interactorPool)
        {
            this.inputService = inputService;
            this.car = car;
            this.carConfig = carConfig;
            this.interactorPool = interactorPool;
        }

        public void Subscribe()
        {
            inputService.OnButtonDown += InputDown;
            inputService.OnButtonUp += InputUp;
        }

        public void Unsubscribe()
        {
            inputService.OnButtonDown -= InputDown;
            inputService.OnButtonUp -= InputUp;
        }

        void InputDown()
        {
            isShooting = true;
            Shot();
        }

        void InputUp()
        {
            isShooting = false;
        }

        public void ShotUpdate()
        {
            if (!isShooting) return;

            timer += Time.deltaTime;
            if (timer >= carConfig.fireInterval)
                Shot();
        }

        void Shot()
        {
            timer = 0f;

            Projectile projectile = interactorPool.Get<Projectile>().Get();
            projectile.Initialize(car.FirePoint);
            
            car.PlayTurretFire();
        }
    }
}