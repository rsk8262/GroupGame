using UnityEngine;
using UnityEngine.UI;
public class HealthBarPlayer : MonoBehaviour
{
    public Slider healthSlider;
    public Gradient healthGradient;
    public Image Fill;
    private void Start()
    {
        Fill.color = healthGradient.Evaluate(1f);
    }
    public void SetHealth(int health)
    {
        healthSlider.value = health;
        Fill.color = healthGradient.Evaluate(healthSlider.normalizedValue);
    }
}
