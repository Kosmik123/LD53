using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBackground : MonoBehaviour
{
    [SerializeField]
    private new Camera camera;
    [SerializeField]
    private SpriteRenderer backgroundRenderer;


    private void Update()
    {
        UpdateSize();
    }

    private void UpdateSize()
    {
        float height = camera.orthographicSize;
        float width = camera.aspect * height;
        backgroundRenderer.transform.localScale = new Vector3(width, height, 1);
    }

    private void UpdateTexture()
    {

    }
}
