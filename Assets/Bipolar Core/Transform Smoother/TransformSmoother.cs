using UnityEngine;

public class TransformSmoother : MonoBehaviour, ITransform
{
    [Header("Settings")]
    [SerializeField]
    private float updateSpeed;

    [Header("Properties")]
    [SerializeField]
    private Vector3 targetPosition;

#if UNITY_EDITOR
    [EulerAngles]
#endif
    [SerializeField]
    private Quaternion targetRotation;

    private float timer;
    private bool isSmoothing;
    public bool IsSmoothing => isSmoothing;

    public Vector3 Position
    {
        get => targetPosition;
        set
        {
            initialPositon = transform.localPosition;
            targetPosition = value;
            StartSmoothing();
        }
    }

    public Quaternion Rotation
    { 
        get => targetRotation;
        set
        {
            initialRotation = transform.localRotation;
            targetRotation = value;
            StartSmoothing();
        }
    }

    private Vector3 currentPositon;
    private Vector3 initialPositon;

    private Quaternion currentRotation;
    private Quaternion initialRotation;

    private void Awake()
    {
        currentRotation = targetRotation = transform.localRotation;
        currentPositon = targetPosition = transform.localPosition;
    }

    private void Update()
    {
        if (isSmoothing == false)
            return;
        CalculateSmoothTransformation(Time.deltaTime);
        transform.localPosition = currentPositon;
        transform.localRotation = currentRotation;
    }

    private void CalculateSmoothTransformation(float deltaTime)
    {
        if (timer < 1)
        {
            timer += deltaTime * updateSpeed;
            currentPositon = Vector3.Lerp(initialPositon, targetPosition, timer);
            currentRotation = new Quaternion(
                Mathf.Lerp(initialRotation.x, targetRotation.x, timer),
                Mathf.Lerp(initialRotation.y, targetRotation.y, timer),
                Mathf.Lerp(initialRotation.z, targetRotation.z, timer),
                Mathf.Lerp(initialRotation.w, targetRotation.w, timer));
        }
        else
        {
            isSmoothing = false;
            currentPositon = targetPosition;
            currentRotation = targetRotation;
        }
    }

    private void StartSmoothing()
    {
        timer = 0;
        isSmoothing = true;
    }

    private void OnValidate()
    {
        Position = targetPosition;
        Rotation = targetRotation;
    }
}
