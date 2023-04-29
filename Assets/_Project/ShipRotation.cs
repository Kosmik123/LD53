using Bipolar.Core.Input;
using UnityEngine;

public class ShipRotation : MonoBehaviour
{
    [SerializeField]
    private Object axisProvider;
    public IAxisInputProvider AxisProvider => axisProvider as IAxisInputProvider;

    [SerializeField]
    private float rotateSpeed;
    public float RotateSpeed
    {
        get => rotateSpeed;
        set => rotateSpeed = value;
    }

    private void Update()
    {
        float horizontal = AxisProvider.GetAxis();
        transform.Rotate(Vector3.back, horizontal * rotateSpeed * Time.deltaTime);
    }
}
