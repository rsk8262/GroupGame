using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{
    public Slider healthSlider;
    [SerializeField] private GameObject healthBarUI;
    public void SetHealth(int health)
    {
        healthSlider.value = health;
    }
    private void LateUpdate()
    {
        transform.LookAt(transform.position + Camera.main.transform.forward);
    }

    public void Disable()
    {
        healthBarUI.SetActive(false);
    }
}
