using NaughtyAttributes;
using UnityEngine;


public class ShipMovement : Rigidbody2DMovement
{
    [SerializeField]
    private float force;
    [SerializeField, Range(0,1)]
    private float backForceMultiplier;

    [Header("Streamline Drag")]
    [SerializeField]
    private float forwardDrag;
    [SerializeField]
    private float backDrag;
    [SerializeField]
    private float transversalDrag;

    [Header("Rotation")]
    [SerializeField]
    private ShipRotation shipRotation;
    [SerializeField]
    private float maxRotateSpeed;
    [SerializeField,]
    private float minRotateSpeed;

    [Header("States")]
    [ShowNonSerializedField, ReadOnly]
    private float throttle;

    [ShowNonSerializedField, ReadOnly]
    private float currentForce;

    [ShowNonSerializedField, ReadOnly]
    private float drag;

    [ShowNonSerializedField, ReadOnly]
    private float streamlinedness;
    public float Streamlinedness => streamlinedness;

    [ShowNonSerializedField, ReadOnly]
    private float longitudinalDrag;

    private void Update()
    {
        throttle = InputProvider.GetVertical();
        CalculateDrag();
        rigidbody.drag = drag;

        float currentMaxForce = Mathf.Max(0, (throttle * maxSpeed * (Vector2)transform.up - rigidbody.velocity).magnitude / Time.deltaTime);
        currentForce = Mathf.Min(currentMaxForce, force);
        if (throttle < 0)
            currentForce *= backForceMultiplier;
        shipRotation.RotateSpeed = Mathf.Lerp(minRotateSpeed, maxRotateSpeed, force == 0 ? 0.5f : rigidbody.velocity.magnitude / maxSpeed);
    }

    private void CalculateDrag()
    {
        streamlinedness = Vector2.Dot(rigidbody.velocity.normalized, transform.up);
        longitudinalDrag = streamlinedness < 0 ? backDrag : forwardDrag;
        drag = Mathf.Lerp(transversalDrag, longitudinalDrag, streamlinedness) - forwardDrag * Mathf.Abs(throttle);
    }

    private void FixedUpdate()
    {
        if (throttle != 0)
        {
            rigidbody.AddForce(Mathf.Sign(throttle) * currentForce * transform.up);
            LimitVelocity();
        }

        MeasureSpeed();
    }
}
