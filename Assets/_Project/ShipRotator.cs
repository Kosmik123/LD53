using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipRotator : MonoBehaviour
{
    [SerializeField]
    private new Rigidbody2D rigidbody;
    [SerializeField]
    private float rotationSpeed;

    private void Reset()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Vector2 direction = rigidbody.velocity;
        if (direction != Vector2.zero)
        {
            float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
            var targetRotation = Quaternion.AngleAxis(angle, Vector3.back);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed);
        }
    }
}
