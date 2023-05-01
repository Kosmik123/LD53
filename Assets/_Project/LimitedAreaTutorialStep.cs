using UnityEngine;
using UnityEngine.Events;

public class LimitedAreaTutorialStep : TutorialStep
{
    [Header("To Link")]
    [SerializeField]
    private LimitedViewController limitedViewController;

    [Header("Settings")]
    [SerializeField]
    private float availableAreaWidth;
    [SerializeField]
    private Vector2 availableAreaCenter;
    [SerializeField]
    private float transitionDuration;
    [SerializeField]
    private GameObject tutorialHint;
    [SerializeField]
    private PlayerInputsChecker playerInputsChecker;

    [SerializeField]
    private UnityEvent onBegin;

    protected override void OnBegin()
    {
        base.OnBegin();
        limitedViewController.ZoomEnabled = false;
        limitedViewController.MoveCamera(availableAreaCenter, transitionDuration);

        limitedViewController.MovementLimited = true;
        limitedViewController.ResizeLimitingArea(availableAreaWidth, transitionDuration);

        if (tutorialHint != null)
            tutorialHint.SetActive(true);

        onBegin.Invoke();
    }

    protected override void OnEnd()
    {
        base.OnEnd();
        //limitedViewController.ZoomEnabled = true;
        limitedViewController.MovementLimited = false;

        if (tutorialHint != null)
            tutorialHint.SetActive(false);
    }
}
