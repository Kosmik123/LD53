using NaughtyAttributes;
using UnityEngine;

public class ProgressLoader : MonoBehaviour
{
    public event System.Action<bool> OnReadyChanged;

    [SerializeField]
    private float loadingSpeed;

    [ShowNonSerializedField, ReadOnly, Range(0,1)]
    private float progress;
    public float Progress
    {
        get => progress;
        set
        {
            if (progress == value)
                return;

            progress = Mathf.Clamp01(value);
            if (progress < 1)
                IsReady = false;
        }
    }

    [ShowNonSerializedField, ReadOnly]
    private bool isReady;
    public bool IsReady 
    { 
        get => isReady;
        private set
        {
            if (value != isReady)
            {
                isReady = value;
                OnReadyChanged?.Invoke(isReady);
            }
        }
    }

    private void Update()
    {
        if (IsReady)
            return;

        Progress += Time.deltaTime * loadingSpeed;
        if (progress >= 1)
        {
            Progress = 1;
            IsReady = true;
        }
    }
}
