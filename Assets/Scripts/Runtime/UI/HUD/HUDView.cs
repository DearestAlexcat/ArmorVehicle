using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ArmorVehicle
{
    
    public class HUDView : MonoBehaviour, IHudView
    {
        [SerializeField] TextMeshProUGUI meterText;
        [SerializeField] RectTransform meterRect;
        [SerializeField] Image progressBar;

        RectTransform progressBarRect;

        void Start()
        {
            Display(false);
        }

        void OnEnable()
        {
            progressBarRect = progressBar.GetComponent<RectTransform>();
        }

        public void UpdateHUD(MovementTracker tracker)
        {
            SetProgress(tracker.Progress);
            SetTraveledDistance(Mathf.RoundToInt(tracker.Distance));
            SetLabelPosition(tracker.Progress);
        }

        public void SetLabelPosition(float value)
        {
            float height = progressBarRect.rect.height;
            Vector3 localPos = meterText.transform.localPosition;
            localPos.y = -height / 2f + height * value;
            meterText.transform.localPosition = localPos;
        }

        public void SetTraveledDistance(int meter)
        {
            meterText.text = $"{meter}m";
        }

        public void SetProgress(float value)
        {
            progressBar.fillAmount = value;
        }

        public void Display(bool value)
        {
            gameObject.SetActive(value);
        }
    }
}