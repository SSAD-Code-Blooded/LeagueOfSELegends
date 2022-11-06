using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public UnityEngine.UI.Slider slider; /**< Slider object that controls health*/
    public Gradient gradient; /**< gradient attribute for color of health bar*/
    public Image fill; /**< Fill of the health bar*/

    ///
    /// function to set the health using an int value
    ///

    public void SetHealth(int health)
    {
        slider.value = health;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    ///
    /// function to set the max health possible
    ///

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
        fill.color = gradient.Evaluate(1f);
    }
    
}
