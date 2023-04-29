using Bipolar.Core.Input;
using UnityEngine;
#if NAUGHTY_ATTRIBUTES
using NaughtyAttributes;
#endif

public class OrthographicCameraZoomController : MonoBehaviour
{
    [SerializeField]
    private new Camera camera;
    [SerializeField]
    private Object axisProvider;
    public IAxisInputProvider AxisProvider
    {
        get => axisProvider as IAxisInputProvider;
        set
        {
            axisProvider = (Object)value;
        }
    }

    [SerializeField]
    private float minSize;
    [SerializeField]
    private float maxSize;
    [SerializeField]
    private float zoomSpeed;

#if NAUGHTY_ATTRIBUTES
    [ShowNonSerializedField, ReadOnly]
#endif
    private float currentZoom;

    private void Start()
    {
        currentZoom = camera.orthographicSize;
    }

    private void Update()
    {
        currentZoom += zoomSpeed * AxisProvider.GetAxis();
        currentZoom = Mathf.Clamp(currentZoom, minSize, maxSize);
        camera.orthographicSize = currentZoom;
    }
}
