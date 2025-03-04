using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Slider _slider;


    public void ShowUnshow(bool off)
    {
        gameObject.SetActive(off);
    }
    public void SeTMaxValue(float value)
    {
        _slider.minValue = value;
        _slider.value = value;
    }
    public void DecreaseValue(float value)
    {
        _slider.value -= value;
    }
}
