using UnityEngine;

namespace ArmorVehicle
{
    public interface IPoolObjectsContainer
    {
        Transform CreatePoolContainer(string poolName);
    }
}