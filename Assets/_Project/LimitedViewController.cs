using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitedViewController : MonoBehaviour
{
    public System.Action OnCameraMovementEnded;

    [SerializeField]
    private new Camera camera;

    [SerializeField]
    private OrthographicCameraZoomController zoomController;

    [SerializeField]
    private MovementLimiter movementLimiter;

    private Vector3 targetPosition;

    public bool ZoomEnabled
    {
        get => zoomController.enabled;
        set
        {
            zoomController.enabled = value;
        }
    }

    public bool MovementLimited
    {
        get => movementLimiter.enabled;
        set
        {
            movementLimiter.enabled = value;
        }
    }

    public Vector2 LimitingSize
    {
        get => movementLimiter.LimitingSize;
    }

    public void MoveCamera(Vector3 targetPosition, float time = 0)
    {
        StopCoroutine(nameof(CameraMovementCo));
        this.targetPosition = targetPosition;
        if (time > 0)
        {
            StartCoroutine(CameraMovementCo(time));
        }
        else
        {
            SetCameraOnTarget();
        }
    }

    public void ResizeLimitingArea(float width, float time = 0)
    {
        StopCoroutine(nameof(ResizeLimitingArea));
        StartCoroutine(AreaResizingCo(width, time));
    }

    private void SetCameraOnTarget()
    {
        camera.transform.position = this.targetPosition;
        OnCameraMovementEnded?.Invoke();
    }

    private IEnumerator CameraMovementCo(float time)
    {
        float timerProgress = 0;
        Vector3 initialPosition = camera.transform.position;
        while (timerProgress < time)
        {
            timerProgress += Time.deltaTime;
            camera.transform.position = Vector3.Lerp(initialPosition, targetPosition, timerProgress / time);
            yield return null;
        }

        SetCameraOnTarget();
    }

    private IEnumerator AreaResizingCo(float width, float time)
    {
        float progress = 0;
        float transitionSpeed = 1 / time;

        float initialCameraSize = camera.orthographicSize;
        float targetCameraSize = 0.5f * width / camera.aspect;

        Vector2 initialAreaSize = LimitingSize;
        Vector2 targetAreaSize = new Vector2(width, targetCameraSize * 2);
        
        while (progress < 1)
        {
            progress += Time.deltaTime * transitionSpeed;
            movementLimiter.LimitingSize = Vector3.Lerp(initialAreaSize, targetAreaSize, progress);
            camera.orthographicSize = Mathf.Lerp(initialCameraSize, targetCameraSize, progress);
            yield return null;
        }
        movementLimiter.LimitingSize = targetAreaSize;
        camera.orthographicSize = targetCameraSize;
    }
}
