using UnityEngine;

namespace ArmorVehicle
{
    public class HealthPointsBar : MonoBehaviour
    {
        [SerializeField] GameObject healthBar;
        [SerializeField] SpriteRenderer bar;

        IHealth health;

        void Start()
        {
            IHealth health = GetComponent<IHealth>();

            if (health != null)
                Construct(health);
            else Debug.LogError("Health component not found");
        }

        void OnEnable()
        {
            Display(false);
        }

        private void OnDestroy()
        {
            if (health != null)
                health.HealthChanged -= UpdateHpBar;
        }

        public void Construct(IHealth health)
        {
            this.health = health;
            health.HealthChanged += UpdateHpBar; 
        }

        public void Display(bool value)
        {
            healthBar.SetActive(value);
        }

        public void UpdateHpBar()
        {
            Display(true);
            bar.material.SetFloat("_FillAmount", Mathf.Clamp01(1f * health.Current / health.Max));
        }
    }
}