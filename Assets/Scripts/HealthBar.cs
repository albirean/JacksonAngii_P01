using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Slider healthBar = null;

    public void SetMaxHealth(int health)
    {
        healthBar.maxValue = health;
    }

    public void SetHealth(int health)
    {
        healthBar.value = health;
    }
}
