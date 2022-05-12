using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaltPouch : MonoBehaviour
{
    public Gradient Gradient;
    public Image Fill;
    [SerializeField] private Slider slider;

    
    public void SetMaxSaltAmount(int amount) {
        slider.maxValue = amount;
        SetSaltAmount(amount);

        Fill.color = Gradient.Evaluate(1f);
    }

    public void SetSaltAmount(int amount) {
        slider.value = amount;
        Fill.color = Gradient.Evaluate(slider.normalizedValue);
    }
}
