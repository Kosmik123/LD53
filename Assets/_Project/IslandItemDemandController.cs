﻿using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;

public class IslandItemDemandController : MonoBehaviour
{
    public event System.Action<Item> OnItemDemanded;
    public event System.Action<Item> OnDemandEnded;

    [Header("To Link")]
    [SerializeField]
    private IslandItemSource islandItemSource;
    [SerializeField]
    private IslandCollisionDetector collisionDetector;

    [Header("Settings")]
    [SerializeField]
    private float maxWaitDuration;
    [SerializeField]
    private float minWaitDuration;
    [SerializeField]
    private bool shouldCommissionAtStart;

    [ShowNonSerializedField, ReadOnly]
    private Item demandedItem;
    public Item DemandedItem => demandedItem;
    public bool IsDemanding => demandedItem != null;

    [SerializeField, ReadOnly]
    private List<Item> possibleItems;

    private void OnEnable()
    {
        collisionDetector.OnCollisionEntered += TryCollectItem;
        islandItemSource.OnItemSet += RemoveItemFromList;
    }

    private void RemoveItemFromList(Item item)
    {
        islandItemSource.OnItemSet -= RemoveItemFromList;
        possibleItems.Remove(item);
    }

    private void Start()
    {
        possibleItems = new List<Item>(ItemsData.Instance.AllItems);
        possibleItems.Remove(islandItemSource.Item);
        if (shouldCommissionAtStart)
            CommissionNextDemand(minWaitDuration: 1);
    }

    public void DemandItem(Item item)
    {
        CancelInvoke();
        demandedItem = item;
        OnItemDemanded?.Invoke(demandedItem);
    }


    [Button]
    private void DemandRandomItem()
    {
        int index = Random.Range(0, possibleItems.Count);
        DemandItem(possibleItems[index]);
    }

    private void TryCollectItem(Collision2D collision)
    {
        if (Inventory.Instance.HasItem(demandedItem) == false)
            return;

        Inventory.Instance.RemoveItem(demandedItem);
        EndDemand();

        CommissionNextDemand(minWaitDuration); 
    }

    private void CommissionNextDemand(float minWaitDuration)
    {
        float timeToNextDemand = Random.Range(minWaitDuration, maxWaitDuration);
        Invoke(nameof(DemandRandomItem), timeToNextDemand);
    }

    [Button]
    private void EndDemand()
    {
        var item = demandedItem;
        demandedItem = null;
        OnDemandEnded?.Invoke(item);
    }

    private void OnDisable()
    {
        collisionDetector.OnCollisionEntered -= TryCollectItem;
    }
}
