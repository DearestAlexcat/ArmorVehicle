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
            return Object.Instantiate(environmentPrefabs[Random.Range(0, environmentPrefabs.Length)]);
        }
    }
}