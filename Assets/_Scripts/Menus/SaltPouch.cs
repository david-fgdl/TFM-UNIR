using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaltPouch : MonoBehaviour
{
    private Slider slider;
    
    public void SetMaxSaltAmount(int amount) {
        slider.maxValue = amount;
        SetSaltAmount(amount);
    }

    public void SetSaltAmount(int amount) {
        slider.value = amount;
    }
}
