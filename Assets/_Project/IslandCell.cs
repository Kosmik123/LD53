using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandCell : MonoBehaviour
{
    [System.Serializable]
    public struct CircularArea
    {
        [field: SerializeField]
        public SpriteRenderer Renderer { get; private set; }
        
        [field: SerializeField]
        public float Radius { get; private set; }
        
        [field: SerializeField]
        public Color Color { get; private set; }
    }

    [SerializeField]
    private CircularArea beach;
    [SerializeField]
    private CircularArea grass;



}
