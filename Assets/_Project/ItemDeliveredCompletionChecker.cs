using NaughtyAttributes;
using UnityEngine;

public class ItemDeliveredCompletionChecker : TutorialStepCompletionChecker
{
    [SerializeField]
    private IslandItemDemandController islandItemDemand;
    [SerializeField]
    private Item requiredItem;
    
    [ReadOnly, ShowNonSerializedField]
    private bool isCompleted;
    
    private void OnEnable()
    {
        islandItemDemand.OnDemandEnded += SetCompleted;
    }

    private void SetCompleted(Item item)
    {
        if (requiredItem == null || requiredItem == item)
        {
            islandItemDemand.OnDemandEnded -= SetCompleted;
            isCompleted = true;
        }
    }

    public override bool IsCompleted()
    {
        return isCompleted;
    }
}
