using UnityEngine;

namespace ArmorVehicle
{
    [CreateAssetMenu(fileName = "ProjectileConfig", menuName = "Configs/ProjectileConfig")]

    public class ProjectileConfig : ScriptableObject
    {
        [Header("Projectile")]
        public float flyTime = 10f;
        public float flySpeed = 15f;
        public int projectileDamage = 55;
    }
}