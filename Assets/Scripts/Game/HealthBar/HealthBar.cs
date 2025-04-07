using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Game.HealthBar
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private TextMeshProUGUI _text;

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void SetMaxValue(float value)
        {
            _slider.maxValue = value;
            _slider.value = value;
        }

        public void DecreaseValue(float value)
        {
            _slider.value -= value;
        }
        public void SetName(string name)
        {
            _text.text = name;
        }
    }
}