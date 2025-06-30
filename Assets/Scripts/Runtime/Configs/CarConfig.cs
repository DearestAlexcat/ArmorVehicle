using UnityEngine;

namespace ArmorVehicle
{
    [CreateAssetMenu(fileName = "CarConfig", menuName = "Configs/CarConfig")]
    public class CarConfig : ScriptableObject
    {
        [Header("Car")]
        public Vector3 carStartPosition;
        public float moveSpeed = 5f;

        [Header("RandomXOffset")]
        public float xOffsetChangeInterval = 2f;
        public float maxOffsetX = 3f;
        public float sideShiftSpeed = 3f;

        [Header("Turret")]
        public float fireInterval = 1f;
        public int damagePerShot = 20;

        [Header("Camera")]
        public float smoothSpeed = 5f;
        public float height = 10f;
        public float zOffset = -2.5f;
        public Vector3 lookOffset;
    }
}