using System.Collections.Generic;
using UnityEngine;

public class IslandItemSource : MonoBehaviour
{
    [SerializeField]
    private Item item;
    public Item Item => item;
    [SerializeField]
    private Island island;
    [SerializeField]
    private ProgressLoader progressLoader;
    [SerializeField]
    private OnCollision2DEnterEvent[] collisionEvents;

    private void Awake()
    {
        island.OnIslandCreated += PopulateCollisionEvents;
    }

    private void PopulateCollisionEvents(Island island)
    {
        island.OnIslandCreated -= PopulateCollisionEvents;
        var cells = island.Cells;
        var collisionEventsList = new List<OnCollision2DEnterEvent>(cells.Count);
        for (int i = 0; i < cells.Count; i++)
        {
            var collisionEvent = cells[i].GetComponentInChildren<OnCollision2DEnterEvent>();
            if (collisionEvent != null)
            {
                collisionEventsList.Add(collisionEvent);
                collisionEvent.OnCollisionEntered += CollisionEvent_OnCollisionEntered;
            }
        }

        collisionEvents = collisionEventsList.ToArray();
    }

    private void CollisionEvent_OnCollisionEntered(Collision2D collision)
    {
        if (progressLoader.IsReady == false)
            return;

        bool added = Inventory.Instance.Add(item);
        if (added)
        {
            progressLoader.Progress = 0;
        }
    }

    private void Start()
    {
        progressLoader.Progress = 1;
    }

    private void OnDestroy()
    {
        foreach (var collisionEvent in collisionEvents)
            collisionEvent.OnCollisionEntered -= CollisionEvent_OnCollisionEntered;
    }
}
