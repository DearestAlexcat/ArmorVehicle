using UnityEngine;

namespace ArmorVehicle
{
    public class CarHealth : Health
    {
        [SerializeField] CarAnimator animator;
        [SerializeField] DamageFlashEffect flashEffect;

        public override void TakeDamage(int damage)
        {
            base.TakeDamage(damage);
            flashEffect.FlashWhite();
            animator.PlayHit();
        }
    }
}