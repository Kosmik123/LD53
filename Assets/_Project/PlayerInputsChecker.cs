using Bipolar.Core.Input;
using NaughtyAttributes;
using UnityEngine;

public class PlayerInputsChecker : TutorialStepCompletionChecker
{
    [System.Flags]
    private enum MovedDirections
    {
        None = 0,
        Up = 1 << 0,
        Down = 1 << 1,
        Left = 1 << 2,
        Right = 1 << 3,
        Vertical = Up | Down,
        Horizontal = Left | Right,
        All = Vertical | Horizontal,
        _AdditionalValueForCorrectSerialization = 1 << 31,
    }

    [SerializeField]
    private Object inputProvider;
    public IMoveInputProvider InputProvider
    {
        get => inputProvider as IMoveInputProvider;
        set => inputProvider = (Object)value;
    }

    [SerializeField]
    private Object axisProvider;
    public IAxisInputProvider AxisProvider => axisProvider as IAxisInputProvider;

    [SerializeField]
    private MovedDirections requiredDirections;

    [ShowNonSerializedField, ReadOnly]
    private MovedDirections movedDirections;
    public bool HasMovedAllDirections => (movedDirections & requiredDirections) >= requiredDirections;
    public override bool IsCompleted() => HasMovedAllDirections;

    private void Update()
    {
        if (InputProvider != null)
        {
            float vertical = InputProvider.GetVertical();
            if (vertical > 0.001f)
                movedDirections |= MovedDirections.Up;
            else if (vertical < -0.001f)
                movedDirections |= MovedDirections.Down;
        }

        if (AxisProvider != null)
        {
            float horizontal = AxisProvider.GetAxis();
            if (horizontal > 0.001f)
                movedDirections |= MovedDirections.Right;
            else if (horizontal < -0.001f)
                movedDirections |= MovedDirections.Left;
        }
    }
}
