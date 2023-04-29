using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandCell : MonoBehaviour
{
    [System.Serializable]
    public struct CircularArea
    {
        [field: SerializeField]
        public SpriteRenderer Renderer { get; set; }
        
        [field: SerializeField]
        public float Radius { get; set; }
        
        [field: SerializeField]
        public Color Color { get; set; }
    
        public void Validate()
        {
            if (Renderer == null)
                return;

            Renderer.color = Color;
            Renderer.transform.localScale = Vector3.one * Radius;
        }
    }

    [SerializeField]
    private CircularArea beach;
    public CircularArea Beach
    {
        get => beach;
        set
        {
            beach = value;
            beach.Validate();
        }
    }

    [SerializeField]
    private CircularArea grass;
    public CircularArea Grass
    {
        get => grass;
        set
        {
            grass = value;
            grass.Validate();
        }
    }

    private void OnValidate()
    {
        beach.Validate();
        grass.Validate();
    }
}
