using UnityEngine;
using NaughtyAttributes;

[CreateAssetMenu]
public class Item : ScriptableObject
{
    [SerializeField, ShowAssetPreview]
    private Sprite icon;
    public Sprite Icon => icon;
}
