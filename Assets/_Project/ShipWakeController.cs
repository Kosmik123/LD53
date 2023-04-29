using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipWakeController : MonoBehaviour
{
    [SerializeField]
    private new ParticleSystem particleSystem;
    [SerializeField]
    private ShipMovement ship;

    [SerializeField]
    private float emmisionBySpeed = 1;

    private void Reset()
    {
        particleSystem = GetComponent<ParticleSystem>();
        ship = GetComponent<ShipMovement>();
    }

    private void Update()
    {
        UpdateParticlesProperties();    
    }

    private void UpdateParticlesProperties()
    {
        var mainModule = particleSystem.main;
        mainModule.startRotation = -Vector2.SignedAngle(Vector2.left, transform.up) * Mathf.Deg2Rad;
        
        var emmision = particleSystem.emission;
        emmision.rateOverTime = ship.Velocity.magnitude * emmisionBySpeed;



    }
}
