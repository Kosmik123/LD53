using System;
using UnityEngine;

public abstract class TutorialStep : MonoBehaviour
{
    [SerializeField]
    private TutorialStepCompletionChecker completionChecker;

    public void Begin()
    {
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
        OnEnd();
    }

}
