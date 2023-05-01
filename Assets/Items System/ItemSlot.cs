using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSlot : MonoBehaviour
{
    [SerializeField] private Item item;
    [SerializeField] private ItemIcon itemIcon;

    public void SetItemIcon(Item item)
    {
        if(item.Icon == null)
        {
            itemIcon.Sprite = null;
        }
        else
        {
            itemIcon.Sprite = item.Icon;
            this.item = item;
        }
    }
}
