using System;
using UnityEngine;

namespace ArmorVehicle
{ 
    public interface IPoolReturnable<T> where T : Component
    {
        event Action<T> OnReturned;
    }
}