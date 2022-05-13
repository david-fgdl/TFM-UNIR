using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    [SerializeField] private Image mIcon;
    [SerializeField] private GameObject mCombinableIcon;
    [SerializeField] private TMP_Text mLabel;

    [SerializeField] private GameObject mStackObject;
    [SerializeField] private TMP_Text mStackLabel;


    public void Set(InventoryItem item) {

        mIcon.sprite = item.data.Icon;
        mLabel.text = item.data.DisplayName;

        if (item.data.IsCombinable) {
            mCombinableIcon.SetActive(true);
        }


        if (item.stackSize <= 1) {
            mStackObject.SetActive(false);
            return;
        }

        mStackLabel.text = item.stackSize.ToString();

    }
}
