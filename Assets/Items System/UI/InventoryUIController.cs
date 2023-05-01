using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUIController : MonoBehaviour
{
    [SerializeField] private Inventory inventory;
    [SerializeField] private List <ItemSlot> itemSlots;
    [SerializeField] private int newItemWindowCapacity;
    [SerializeField] private int currentItemWindowCapacity;

    private void Update()
    {
        ActiveInventorySlot();
    }

    private void Start()
    {
        ActiveInventorySlot();
    }

    private void OnEnable()
    {
        inventory.OnItemAdded += RefreshItems;
        inventory.OnItemRemoved += RefreshItems;
    }

    private void RefreshItems(Item item)
    {
        for (int i = 0; i < inventory.Capacity; i++)
        {
            if (i < inventory.Items.Count)
                itemSlots[i].SetItemIcon(inventory.Items[i]);
            else
                itemSlots[i].SetItemIcon(null);
        }
    }

    private void OnDisable()
    {
        inventory.OnItemAdded -= RefreshItems;
        inventory.OnItemRemoved -= RefreshItems;

    }

    private void ActiveInventorySlot()
    {
        newItemWindowCapacity = inventory.Capacity;

        if (currentItemWindowCapacity != newItemWindowCapacity)
        {
            for (int i = 0; i < newItemWindowCapacity; i++)
            {
                itemSlots[i].gameObject.SetActive(true);
            }

            for (int z = newItemWindowCapacity; z < itemSlots.Count; z++)
            {
                itemSlots[z].gameObject.SetActive(false);
            }
            currentItemWindowCapacity = newItemWindowCapacity;
        }
    }
}
