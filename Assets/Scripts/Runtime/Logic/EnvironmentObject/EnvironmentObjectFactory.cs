using UnityEngine;

namespace ArmorVehicle
{
    public class EnvironmentObjectFactory : IObjectFactory<EnvironmentObject>
    {
        private EnvironmentObject[] environmentPrefabs;

        public EnvironmentObjectFactory(LevelConfig config)
        {
            environmentPrefabs = config.environmentObjects;
        }

        public EnvironmentObject Create()
        {
            if (environmentPrefabs.Length == 0) return null;
            return Object.Instantiate(environmentPrefabs[Random.Range(0, environmentPrefabs.Length)]);
        }
    }
}