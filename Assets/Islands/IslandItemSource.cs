using System.Collections.Generic;
using UnityEngine;

public class IslandItemSource : MonoBehaviour
{
    public event System.Action<Item> OnItemSet;
    public event System.Action<Item> OnItemCollected;

    [SerializeField]
    private Item item;
    public Item Item => item;
    [SerializeField]
    private Island island;
    public Island Island => island;
    [SerializeField]
    private ProgressLoader progressLoader;
    [SerializeField]
    private IslandCollisionDetector collisionDetector;

    private void Awake()
    {
        if (island.IsInited)
            PopulateCollisionEvents(island);
        else
            island.OnIslandCreated += PopulateCollisionEvents;
    }

    private void OnEnable()
    {
        collisionDetector.OnCollisionEntered += CollisionDetector_OnCollisionEntered;
    }
    private void Start()
    {
        progressLoader.Progress = 1;
        if (item == null)
        {
            int index = Random.Range(0, ItemsData.Instance.AllItems.Length);
            item = ItemsData.Instance.AllItems[index];
        }
        OnItemSet?.Invoke(item);
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
            }
        }

        collisionDetector.CollisionEvents = collisionEventsList.ToArray();
    }

    private void CollisionDetector_OnCollisionEntered(Collision2D collision)
    {
        if (progressLoader.IsReady == false)
            return;

        bool added = Inventory.Instance.Add(item);
        if (added)
        {
            progressLoader.Progress = 0;
            OnItemCollected?.Invoke(item);
        }
    }

    private void OnDisable()
    {
        collisionDetector.OnCollisionEntered -= CollisionDetector_OnCollisionEntered;
    }

}
