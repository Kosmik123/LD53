using System;
using UnityEngine;
using UnityEngine.UI;

public class ItemIcon : MonoBehaviour
{
    [Header("To Link")]
    [SerializeField]
    private Image loadedImage;
    [SerializeField]
    private Image unloadedImage;

    [Header("Settings")]
    [SerializeField]
    private Sprite sprite;
    public Sprite Sprite
    {
        get => sprite;
        set
        {
            sprite = value;
            Validate();
        }
    }

    [SerializeField, Range(0, 1)]
    private float progress;
    public float Progress
    {
        get => progress;
        set
        {
            progress = value;
            Validate();
        }
    }

    private void OnEnable()
    {
        loadedImage.enabled = unloadedImage.enabled = true;
    }

    private void OnDisable()
    {
        loadedImage.enabled = unloadedImage.enabled = false;
    }

    private void OnValidate()
    {
        Validate();
    }

    private void Validate()
    {
        loadedImage.fillAmount = progress;
        loadedImage.sprite = unloadedImage.sprite = sprite;
    }
}
