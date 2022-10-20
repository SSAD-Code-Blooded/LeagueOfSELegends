using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public UnityEngine.UI.Slider slider;

    public void SetHealth(int health)
    {
        slider.value = health;
    }
    
}
