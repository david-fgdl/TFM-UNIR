using UnityEngine;
using UnityEngine.UI;

public class SaltPouch : MonoBehaviour
{
    public Gradient Gradient;
    public Image Fill;
    [SerializeField] private Slider _slider;

    
    public void SetMaxSaltAmount(int amount) {
        _slider.maxValue = amount;
        SetSaltAmount(amount);

        Fill.color = Gradient.Evaluate(1f);
    }

    public void SetSaltAmount(int amount) {
        _slider.value = amount;
        Fill.color = Gradient.Evaluate(_slider.normalizedValue);
    }

}
