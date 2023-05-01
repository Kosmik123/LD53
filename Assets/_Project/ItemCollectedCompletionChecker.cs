using NaughtyAttributes;
using UnityEngine;

public class ItemCollectedCompletionChecker : TutorialStepCompletionChecker
{
    [SerializeField]
    private IslandItemSource islandItemSource;

    [ShowNonSerializedField, ReadOnly]
    private bool collected;

    private void OnEnable()
    {
        islandItemSource.OnItemCollected += SetAsCollected;
    }

    private void SetAsCollected(Item item)
    {
        collected = true;
    }

    public override bool IsCompleted()
    {
        if (collected == false)
            return false;

        collected = false;
        return true;
    }

    private void OnDisable()
    {
        islandItemSource.OnItemCollected -= SetAsCollected;
    }
}
