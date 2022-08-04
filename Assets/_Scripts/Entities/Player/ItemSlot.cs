using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private GameObject _combinableIcon;
    [SerializeField] private TMP_Text _label;
    [SerializeField] private GameObject _stackObject;
    [SerializeField] private TMP_Text _stackLabel;


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
