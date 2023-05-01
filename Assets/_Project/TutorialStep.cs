using System;
using UnityEngine;

public abstract class TutorialStep : MonoBehaviour
{
    [SerializeField]
    private TutorialStepCompletionChecker completionChecker;

    private void Awake()
    {
        completionChecker.enabled = false;
    }

    public void Begin()
    {
        completionChecker.enabled = true;
        OnBegin();
    }

    protected virtual void OnBegin() { }
    
    public virtual bool IsCompleted()
    {
        return completionChecker.IsCompleted();
    }

    protected virtual void OnEnd() { }

    public void End()
    {
        completionChecker.enabled = false;
        OnEnd();
    }
}
