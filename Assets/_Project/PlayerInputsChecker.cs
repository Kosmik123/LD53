using Bipolar.Core.Input;
using NaughtyAttributes;
using UnityEngine;

public class PlayerInputsChecker : MonoBehaviour
{
    private enum MovedDirections
    {
        None = 0,
        Up = 1 << 0,
        Down = 1 << 1,
        Left = 1 << 2,
        Right = 1 << 3,
        All = Up | Down | Left | Right,
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

    [ShowNonSerializedField, ReadOnly]
    private MovedDirections movedDirections;
    public bool HasMovedAllDirections => movedDirections == MovedDirections.All;

    private void Update()
    {
        float vertical = InputProvider.GetVertical();
        if (vertical > 0.5f)
            movedDirections |= MovedDirections.Up;
        else if (vertical < -0.5f)
            movedDirections |= MovedDirections.Down;

        float horizontal = AxisProvider.GetAxis();
        if (horizontal > 0.5f)
            movedDirections |= MovedDirections.Right;
        else if (horizontal < -0.5)
            movedDirections |= MovedDirections.Left;
    }
}
