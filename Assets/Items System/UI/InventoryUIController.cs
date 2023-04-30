using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUIController : MonoBehaviour
{
    [SerializeField] private Inventory inventory;
    [SerializeField] private List <GameObject> itemSlots;
    [SerializeField] private int itemWindowCapacity;
    [SerializeField] private int currentItemWindowCapacity;

    private void Update()
    {
        ActiveInventorySlot(itemWindowCapacity);
    }

    private void Start()
    {
        ActiveInventorySlot(itemWindowCapacity);
    }

    private void ActiveInventorySlot(int itemWindowCapacity)
    {
        itemWindowCapacity = inventory.Capacity;

        if (currentItemWindowCapacity != itemWindowCapacity)
        {
            foreach (var itemSlot in itemSlots)
            {
                itemSlots[itemWindowCapacity].gameObject.SetActive(true);
                currentItemWindowCapacity = itemWindowCapacity;
            }
        }
    }
}
