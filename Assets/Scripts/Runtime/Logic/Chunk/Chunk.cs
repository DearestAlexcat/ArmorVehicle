using System.Collections.Generic;
using UnityEngine;

namespace ArmorVehicle
{
    public class Chunk : MonoBehaviour
    {
        [field: SerializeField] public Transform Begin { get; private set; }
        [field: SerializeField] public Transform End { get; private set; }
        [field: SerializeField] public List<Vector3> environmentObjectPositions;

        [HideInInspector] public float spawnZoneCenter;

        public float spawnZoneRadius;
   
        public float GetDistance()
        {
            return Vector3.Distance(Begin.position, End.position);
        }

        public bool EnvironmentObjectIsEmpty()
        {
            return environmentObjectPositions.Count == 0;
        }

        public Vector3 GetSpawnZonePosition()
        {
            float distanceByZ = End.position.z - Begin.position.z;
            return new Vector3(0f, 0f, (distanceByZ * spawnZoneCenter) + Begin.position.z);
        }

        public IEnumerable<Vector3> GetEnvironmentObjectPositions()
        {
            foreach (var position in environmentObjectPositions)
                yield return transform.TransformPoint(position);
        }
    }

    [System.Serializable]
    public class EnvironmentObjectOrientation
    {
        public Vector3 position;
        public Vector3 rotation;
    }
}

