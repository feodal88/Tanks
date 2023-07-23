using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    int maxHealth;

    Slider slider;

    public void Init(int maxHealth)
    {
        slider = GetComponentInChildren<Slider>();

        this.maxHealth = maxHealth;

        slider.maxValue = maxHealth;
        slider.value = maxHealth;
        UpdateHealthBar(maxHealth);
    }

    public void UpdateHealthBar(int health)
    {
        slider.value = health;
        // 100 % HP: red 146, green 255, blue 28
        float r = 146f / 255;
        float g = Convert.ToSingle(health) / maxHealth;
        float b = 28f / 255;

        Image fillArea = slider.fillRect.GetComponent<Image>();
        fillArea.color = new Color(r, g, b);
    }
}
