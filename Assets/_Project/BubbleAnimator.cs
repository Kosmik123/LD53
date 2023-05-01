using NaughtyAttributes;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class BubbleAnimator : MonoBehaviour
{
    [SerializeField]
    private float shakeInterval;

    [SerializeField]
    private float shakeStrength;
    [SerializeField]
    private float singleShakeDuration;


    private RectTransform rectTransform;
    private Image[] allImages;

    private void Awake()
    {
        rectTransform = (RectTransform)transform;
        allImages = GetComponentsInChildren<Image>();
        Hide(0);
    }

    [Button]
    public void Show()
    {
        gameObject.SetActive(true);
        float width = rectTransform.sizeDelta.x;
        rectTransform.sizeDelta = new Vector2(width, 0);
        var animation = DOTween.Sequence();
        animation
            .Append(rectTransform.DOSizeDelta(width * new Vector2(1, 1.1f), 0.4f))
            .Append(rectTransform.DOSizeDelta(width * new Vector2(1, 0.93f), 0.2f))
            .Append(rectTransform.DOSizeDelta(width * new Vector2(1, 1.03f), 0.1f))
            .Append(rectTransform.DOSizeDelta(width * new Vector2(1, 1.1f), 0.05f))
            .AppendCallback(StartShaking);
    }

    private void StartShaking()
    {
        InvokeRepeating(nameof(Shake), shakeInterval, shakeInterval);
    }

    [Button]
    public void Hide(float duration = 0.2f)
    {
        CancelInvoke(nameof(Shake));
        var animation = DOTween.Sequence();
        for (int i = 0; i < allImages.Length; i++)
            animation.Join(allImages[i].DOFade(0, duration));

        animation.OnComplete(DeactivateObject);
    }

    private void DeactivateObject()
    {
        gameObject.SetActive(false);
        for (int i = 0; i < allImages.Length; i++)
        {
            var color = allImages[i].color;
            color.a = 1;
            allImages[i].color = color;
        };
    }

    private void Shake()
    {
        var animation = DOTween.Sequence();
        animation
            .Append(rectTransform.DORotate(Vector3.forward * shakeStrength, singleShakeDuration))
            .Append(rectTransform.DORotate(Vector3.forward * -shakeStrength, 2 * singleShakeDuration))
            .Append(rectTransform.DORotate(Vector3.forward * shakeStrength, 2 * singleShakeDuration))
            .Append(rectTransform.DORotate(Vector3.forward * -shakeStrength, 2 * singleShakeDuration))
            .Append(rectTransform.DORotate(Vector3.zero, singleShakeDuration));
    }

}
