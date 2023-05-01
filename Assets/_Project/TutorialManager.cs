using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField]
    private TutorialStep[] steps;

    [ShowNonSerializedField, ReadOnly]
    private int currentStepIndex = -1;

    private void Awake()
    {
        currentStepIndex = -1;
    }

    private void Start()
    {
        BeginNextStep();
    }

    private void BeginNextStep()
    {
        if (enabled == false)
            return;

        if (currentStepIndex < steps.Length - 1)
        {
            currentStepIndex++;
            steps[currentStepIndex].Begin();
        }
        else
        {
            enabled = false;
        }
    }

    private void Update()
    {
        var currentStep = steps[currentStepIndex];
        if (currentStep.IsCompleted())
        {
            currentStep.End();
            BeginNextStep();
        }
    }

}
