using UnityEngine;

[CreateAssetMenu]
public class IslandSettings : ScriptableObject
{
    [field: SerializeField]
    public int MinCellsCount { get; private set; }

    [field: SerializeField]
    public int MaxCellsCount { get; private set; }

    [field: SerializeField]
    public float MaxIslandRadius { get; private set; }

    [field: SerializeField]
    public float MinCellRadius { get; private set; }

    [field: SerializeField]
    public float MaxCellRadius { get; private set; }

    [field: SerializeField, Range(0, 1)]
    public float MinCellGrassRelativeRadius { get; private set; }

    [field: SerializeField, Range(0, 1)]
    public float MaxCellGrassRelativeRadius { get; private set; }
}
