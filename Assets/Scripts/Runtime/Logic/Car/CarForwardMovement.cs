using UnityEngine;

namespace ArmorVehicle
{
    public class CarForwardMovement
    {
        readonly Car car;
        readonly CarConfig config;

        float targetXOffset = 0f;
        float timeSinceLastShift = 0f;

        public CarForwardMovement(Car car, CarConfig config)
        {
            this.car = car;
            this.config = config;
        }

        void ApplyRandomXOffset()
        {
            timeSinceLastShift += Time.deltaTime;
            if (timeSinceLastShift >= config.xOffsetChangeInterval)
            {
                targetXOffset = Mathf.Clamp(RandomService.NextGaussian(0f, config.maxOffsetX / 3f), -config.maxOffsetX, config.maxOffsetX);
                timeSinceLastShift = 0f;
            }

            Vector3 currentPos = car.CarMover.position;
            float newX = Mathf.Lerp(currentPos.x, targetXOffset, Time.deltaTime * config.sideShiftSpeed);

            car.CarMover.position = new Vector3(newX, currentPos.y, currentPos.z);
        }

        void MoveForward()
        {
            car.CarMover.Translate(Vector3.forward * config.moveSpeed * Time.deltaTime);
        }

        public void UpdatePosition()
        {
            MoveForward();
            ApplyRandomXOffset();

            car.transform.position = Vector3.MoveTowards(car.transform.position, car.CarMover.position, config.moveSpeed * Time.deltaTime);
            Vector3 direction = car.CarMover.position - car.transform.position;
            if (direction != Vector3.zero)
                car.transform.forward = direction.normalized;
        }
    }
}
