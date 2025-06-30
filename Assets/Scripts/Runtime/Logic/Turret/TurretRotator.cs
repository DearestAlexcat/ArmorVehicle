using UnityEngine;

namespace ArmorVehicle
{
    public class TurretRotator
    {
        readonly IInputService inputService;

        [SerializeField] float rotationSensitivity = 0.1f;
        [SerializeField] float minRotationAngle = -45f;
        [SerializeField] float maxRotationAngle = 45f;

        Transform turret;
        float currentRotationY;

        public TurretRotator(Car car, IInputService inputService, IStaticDataService staticDataService)
        {
            this.inputService = inputService;

            turret = car.Turret;
            currentRotationY = NormalizeAngle(turret.localEulerAngles.y);
        }

        public void Subscribe()
        {
            inputService.OnSwipe += RotateByInput;
        }

        public void Unsubscribe()
        {
            inputService.OnSwipe -= RotateByInput;
        }

        public void RotateByInput(float deltaX)
        {
            float deltaRotation = deltaX * rotationSensitivity;
            currentRotationY -= deltaRotation;
            currentRotationY = Mathf.Clamp(currentRotationY, minRotationAngle, maxRotationAngle);

            turret.localEulerAngles = new Vector3(0f, currentRotationY, 0f);
        }


        // Normalize angle in composition -180..180
        private float NormalizeAngle(float angle)
        {
            angle %= 360f;
            if (angle > 180f) angle -= 360f;
            return angle;
        }
    }
}