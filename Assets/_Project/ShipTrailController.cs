using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipTrailController : MonoBehaviour
{
    [SerializeField]
    private TrailRenderer trailRenderer;
    [SerializeField]
    private ShipMovement ship;

    [Header("Settings")]
    [SerializeField]
    private float sideWidth;
    [SerializeField]
    private float sternWidth;
    [SerializeField]
    private float lengthBySpeedMultiplier = 1;

    private void Update()
    {
        trailRenderer.widthMultiplier = Mathf.Lerp(sideWidth, sternWidth, Mathf.Abs(ship.Streamlinedness));
        float shipSpeed = ship.Velocity.magnitude;
        //trailRenderer.time = shipSpeed * lengthBySpeedMultiplier;
        trailRenderer.emitting = shipSpeed > 1;
    }
}
