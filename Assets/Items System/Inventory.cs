using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance { get; private set; }

    public event System.Action<Item> OnItemAdded;
    public event System.Action<Item> OnItemRemoved;

    [SerializeField]
    private List<Item> items;
    public List<Item> Items => items;

    [SerializeField]
    private int capacity;
    public int Capacity
    {
        get => capacity;
        set
        {
            capacity = value;
        }
    }


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }

    public bool Add(Item item)
    {
        if (Items.Count >= Capacity)
            return false;

        Items.Add(item);
        OnItemAdded?.Invoke(item);
        return true;
    }

    public bool HasItem(Item item)
    {
        return Items.Contains(item);
    }

    public bool RemoveItem(Item item)
    {
        int itemIndex = Items.IndexOf(item);
        if (itemIndex < 0)
            return false;

        return RemoveItem(itemIndex);
    }

    private bool RemoveItem(int itemIndex)
    {
        var item = Items[itemIndex];
        Items.RemoveAt(itemIndex);
        OnItemRemoved?.Invoke(item);
        return true;
    }





}
