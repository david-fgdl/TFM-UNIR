/* SCRIPT QUE CONTROLA LA BOLSA DE SAL */

using UnityEngine;
using UnityEngine.UI;

public class SaltPouch : MonoBehaviour
{

    /* VARIABLES */

    // VALORES GRAFICOS
    public Gradient Gradient;
    public Image Fill;
    [SerializeField] private Slider _slider;

    /* METODOS PARA CONTROLAR LA SAL */

    // METODO PARA FIJAR EL VALOR MAXIMO DE SAL
    public void SetMaxSaltAmount(int amount) {

        _slider.maxValue = amount;
        SetSaltAmount(amount);

        Fill.color = Gradient.Evaluate(1f);
    }

    // METODO PARA FIJAR EL VALOR ACTUAL DE SAL
    public void SetSaltAmount(int amount) {

        _slider.value = amount;
        Fill.color = Gradient.Evaluate(_slider.normalizedValue);

    }

}
