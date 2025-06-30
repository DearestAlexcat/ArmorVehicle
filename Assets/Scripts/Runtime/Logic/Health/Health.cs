using System;
using UnityEngine;

namespace ArmorVehicle
{
    public class Health : MonoBehaviour, IHealth
    {
        [SerializeField] int current;
        [SerializeField] int max;

        public event Action HealthChanged;

        public int Current
        {
            get => current;
            set => current = value;
        }

        public int Max
        {
            get => max;
            set => max = value;
        }

        public virtual void TakeDamage(int damage)
        {
            Current -= damage;
            HealthChanged?.Invoke();
        }

        public virtual void TakeDamage(int damage, Vector3 direction)
        {
            TakeDamage(damage);
        }
    }
}