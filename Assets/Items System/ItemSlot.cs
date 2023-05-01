using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSlot : MonoBehaviour
{
    [SerializeField] private Item item;
    [SerializeField] private ItemIcon itemIcon;

    public void SetItemIcon(Item item)
    {
        if (item == null)
        {
            itemIcon.Sprite = null;
            itemIcon.enabled = false;
        }
        else
        {
            itemIcon.Sprite = item.Icon;
            itemIcon.enabled = true;
            this.item = item;
        }
    }
}
