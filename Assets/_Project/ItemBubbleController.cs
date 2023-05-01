using NaughtyAttributes;
using UnityEngine;

public class ItemBubbleController : MonoBehaviour
{
    [SerializeField]
    private BubbleAnimator bubbleAnimator;
    [SerializeField]
    private IslandItemDemandController itemDemandController;
    [SerializeField]
    private ItemIcon itemIcon;

    [ShowNonSerializedField, ReadOnly]
    private Item currentlyDemandedItem;

    private void OnEnable()
    {
        itemDemandController.OnItemDemanded += SetDemandedItem;
        itemDemandController.OnDemandEnded += UnsetDemandedItem;
    }

    private void Start()
    {
        if (itemDemandController.IsDemanding)
            SetDemandedItem(itemDemandController.DemandedItem);
        else
            UnsetDemandedItem();
    }

    private void SetDemandedItem(Item item)
    {
        if (item == null)
            return;

        currentlyDemandedItem = item;
        itemIcon.Sprite = item.Icon;
        bubbleAnimator.Show();
    }

    private void UnsetDemandedItem()
    {
        bubbleAnimator.Hide();
        Invoke(nameof(SetNoneItemIcon), 0.15f);
    }

    private void SetNoneItemIcon()
    {
        itemIcon.Sprite = null;
    }

    private void OnDisable()
    {
        itemDemandController.OnItemDemanded -= SetDemandedItem;
        itemDemandController.OnDemandEnded -= UnsetDemandedItem;
    }
}
