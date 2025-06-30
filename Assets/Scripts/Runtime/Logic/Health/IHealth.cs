using System;
using UnityEngine;

namespace ArmorVehicle
{
    public interface IHealth
    {
        event Action HealthChanged;
        int Current { get; set; }
        int Max { get; set; }
        void TakeDamage(int damage);
        void TakeDamage(int damage, Vector3 direction);
    }
}