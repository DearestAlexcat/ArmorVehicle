using UnityEngine;

namespace ArmorVehicle
{
    [CreateAssetMenu(fileName = "EnemyConfig", menuName = "Configs/EnemyConfig")]
    public class EnemyConfig : ScriptableObject
    {
        public int hp = 100;
        public float speed = 3.5f;
        public float walkingSpeed = 2f;
        public int damage = 20;
        public float triggerDistance = 30;
        public float idleDuration = 2f;
        public float wanderDuration = 2f;
        public Vector2 bloodDebrisSpeed = new Vector2(30f, 100f);
        public float bloodDebrisSpread = 0.3f;
    }
}