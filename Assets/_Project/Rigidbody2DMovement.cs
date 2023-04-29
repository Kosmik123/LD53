using UnityEngine;
using Bipolar.Core;
using NaughtyAttributes;

public class Rigidbody2DMovement : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField]
    protected new Rigidbody2D rigidbody;
    public Vector2 Velocity => rigidbody.velocity;

    [SerializeField]
    private Object inputProvider;
    public IMoveInputProvider InputProvider
    {
        get => inputProvider as IMoveInputProvider;
        set => inputProvider = (Object)value;
    }

    [SerializeField]
    protected float maxSpeed;

    [ShowNonSerializedField, ReadOnly]
    private float currentSpeed;
    private Vector3 previousPosition;

    private void Reset()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        InputProvider = GetComponent<IMoveInputProvider>();
    }

    protected void LimitVelocity()
    {
        if (rigidbody.velocity.sqrMagnitude > maxSpeed * maxSpeed)
            rigidbody.velocity = rigidbody.velocity.normalized * maxSpeed;
    }

    protected void MeasureSpeed()
    {
        float distance = (transform.position - previousPosition).magnitude;
        currentSpeed = distance / Time.deltaTime;
        previousPosition = transform.position;
    }

}
