using System;
using UnityEngine;

namespace ArmorVehicle
{
    [RequireComponent(typeof(Animator))]
    public class EnemyAnimator : MonoBehaviour
    {
        readonly int Idle = Animator.StringToHash("Idle");
        readonly int Running = Animator.StringToHash("Running");
        readonly int Hit = Animator.StringToHash("Hit");
        readonly int Walking = Animator.StringToHash("Walking");

        Animator animator;
        Action onEndHitAnimation;

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        public void PlayIdle(bool value)
        {
            animator.SetBool(Idle, value);
        }


        public void PlayHit(Action onEndHitAnimation = null)
        {
            this.onEndHitAnimation = onEndHitAnimation;
            animator.SetTrigger(Hit);
        }

        public void OnExitHitState()
        {
            onEndHitAnimation?.Invoke();
        }

        public void PlayMove(bool value)
        {
            animator.SetBool(Running, value);
        }

        public void PlayWalking(bool value)
        {
            animator.SetBool(Walking, value);
        }
    }
}