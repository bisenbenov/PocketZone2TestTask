using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider _slider;

    public void SetHealthValue(int valueHealth, int valueMaxHealth)
    {
        _slider.value = valueHealth;
        _slider.maxValue = valueMaxHealth;
    }
}
