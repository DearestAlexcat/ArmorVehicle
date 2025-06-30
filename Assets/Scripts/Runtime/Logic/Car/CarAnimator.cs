using UnityEngine;

namespace ArmorVehicle
{
    [RequireComponent(typeof(Animator))]
    public class CarAnimator : MonoBehaviour
    {
        readonly int carReceiveDamage = Animator.StringToHash("ReceiveDamage");

        private Animator animator;

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        public void PlayHit()
        {
            animator.SetTrigger(carReceiveDamage);
        }
    }
}