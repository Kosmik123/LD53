using UnityEngine;

public class OnCollision2DEnterEvent : MonoBehaviour
{
    public event System.Action<Collision2D> OnCollisionEntered;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        OnCollisionEntered?.Invoke(collision);
    }
}
