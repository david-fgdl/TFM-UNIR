/* SCRIPT PARA CONTROLAR EL COMPORTAMIENTO DE LAS RANURAS DE OBJETOS */

using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{

    // ELEMENTOS
    [SerializeField] private Image _icon;
    [SerializeField] private GameObject _combinableIcon;
    [SerializeField] private TMP_Text _label;
    [SerializeField] private GameObject _stackObject;
    [SerializeField] private TMP_Text _stackLabel;

    /* METODOS DE LA RANURA DE OBEJTOS*/

    // METODO PARA ESTABLECER EL ESTADO DE LA RANURA DE OBJETOS
    public void Set(InventoryItem item) 
    {

        _icon.sprite = item.Data.Icon;
        _label.text = item.Data.DisplayName;

        if (item.Data.IsCombinable) 
        {
            _combinableIcon.SetActive(true);
        }

        if (item.StackSize <= 1) 
        {
            _stackObject.SetActive(false);
            return;
        }

        _stackLabel.text = item.StackSize.ToString();

    }

}
