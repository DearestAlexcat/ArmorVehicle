using System;
using UnityEngine;

namespace ArmorVehicle
{
    public class EnvironmentObject : MonoBehaviour, IPoolReturnable<EnvironmentObject>
    {
        public event Action<EnvironmentObject> OnReturned;

        public void ReturnToPool()
        {
            OnReturned?.Invoke(this);
        }
    }
}