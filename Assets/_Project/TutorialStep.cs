using System;
using UnityEngine;

public abstract class TutorialStep : MonoBehaviour
{
    public void Begin()
    {
        OnBegin();
    }

    protected virtual void OnBegin() { }
    public abstract bool IsCompleted();
    protected virtual void OnEnd() { }

    public void End()
    {
        OnEnd();
    }

}
