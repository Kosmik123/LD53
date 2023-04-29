using UnityEngine;
using NaughtyAttributes;

public class PlayerMovement : Rigidbody2DMovement
{
    [SerializeField]
    private float maxSpeed;
    [SerializeField]
    private float force;
    [SerializeField]
    private float drag;

    [Header("States")]
    [ShowNonSerializedField, ReadOnly]
    private Vector2 direction;
    public Vector2 Direction => direction;
    [ShowNonSerializedField, ReadOnly]
    private float currentForce;
    [ShowNonSerializedField, ReadOnly]
    private float currentMaxForce;

    private void Update()
    {
        float vertical = InputProvider.GetVertical();
        float horizontal = InputProvider.GetHorizontal();
        direction = new Vector2(horizontal, vertical);
        rigidbody.drag = direction == Vector2.zero ? drag : 0;
    }

    private void FixedUpdate()
    {
        if (direction.sqrMagnitude > 1)
            direction.Normalize();

        if (direction != Vector2.zero)
        {
            currentMaxForce = (direction * maxSpeed - rigidbody.velocity).magnitude / Time.fixedDeltaTime;
            currentForce = Mathf.Min(currentMaxForce, force);
            rigidbody.AddForce(direction * currentForce);
            LimitVelocity();
        }

        MeasureSpeed();
    }
}
