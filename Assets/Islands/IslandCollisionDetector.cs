using UnityEngine;

public class IslandCollisionDetector : MonoBehaviour
{
    public event System.Action<Collision2D> OnCollisionEntered;

    [SerializeField]
    private OnCollision2DEnterEvent[] collisionEvents;
    public OnCollision2DEnterEvent[] CollisionEvents
    {
        get => collisionEvents;
        set
        {
            collisionEvents = value;
            SubscribeCollisionEvents();
        }
    }


    private void OnEnable()
    {
        SubscribeCollisionEvents();
    }

    private void SubscribeCollisionEvents()
    {
        foreach (var collisionEvent in collisionEvents)
        {
            collisionEvent.OnCollisionEntered -= CollisionEvent_OnCollisionEntered;
            collisionEvent.OnCollisionEntered += CollisionEvent_OnCollisionEntered;
        }
    }

    private void CollisionEvent_OnCollisionEntered(Collision2D collision)
    {
        OnCollisionEntered?.Invoke(collision);
    }

    private void OnDisable()
    {
        foreach (var collisionEvent in collisionEvents)
            collisionEvent.OnCollisionEntered -= CollisionEvent_OnCollisionEntered;
    }
}
