using UnityEngine;

namespace ArmorVehicle
{
    public class MovementTracker
    {
        float moveSpeed;
        float startTime;
        float travelDistance;

        public void Initialize(float moveSpeed)
        {
            this.moveSpeed = moveSpeed;
        }

        public void Start()
        {
            startTime = Time.time;
        }

        public void AddDistance(float distance)
        {
            travelDistance += distance;
        }

        public float TotalTime => Time.time - startTime;

        public float Distance => moveSpeed * TotalTime;

        public float Progress => Distance / travelDistance;
    }
}