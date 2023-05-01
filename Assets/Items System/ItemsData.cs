using NaughtyAttributes;
using UnityEngine;

[CreateAssetMenu]
public class ItemsData : ScriptableObject
{
    private static ItemsData instance;
    public static ItemsData Instance
    {
        get
        {
            return instance;
        }
        private set => instance = value;
    }

    [field: SerializeField]
    public Item[] AllItems { get; private set; }

    public void Awake()
    {
        Instance = this;
    }
}

