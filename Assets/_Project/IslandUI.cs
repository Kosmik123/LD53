using System;
using UnityEngine;

public class IslandUI : MonoBehaviour
{
    [SerializeField]
    private Canvas canvas;
    [SerializeField]
    private ItemIcon itemIcon;
    [SerializeField]
    private IslandItemSource itemSource;
    [SerializeField]
    private ProgressLoader progressLoader;
    [SerializeField]
    private GameObject itemWindow;


    private void Awake()
    {
        canvas.worldCamera = Camera.main;
        itemSource.OnItemSet += SetItem;
    }

    private void SetItem(Item item)
    {
        itemSource.OnItemSet -= SetItem;
        itemIcon.Sprite = item.Icon;
    }

    private void Update()
    {
        if (itemWindow.activeSelf != itemSource.enabled)
            itemWindow.SetActive(itemSource.enabled);
        itemIcon.Progress = progressLoader.Progress;
    }
}
