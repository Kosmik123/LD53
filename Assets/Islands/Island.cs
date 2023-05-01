using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;

public class Island : MonoBehaviour
{
    public event System.Action<Island> OnIslandCreated;

    [SerializeField]
    private IslandCell[] cells;
    public IReadOnlyList<IslandCell> Cells => cells;

    [field: ShowNonSerializedField, ReadOnly]
    public bool IsInited { get; private set; } = false;

    private void Start()
    {
        if (IsInited == false)
            Init(GetComponentsInChildren<IslandCell>());
    }

    [ContextMenu("Clear")]
    private void ClearInEditor()
    { 
        foreach (var cell in cells)
            DestroyImmediate(cell.gameObject);
        
        cells = null;
    }

    public void Init(IReadOnlyList<IslandCell> cells)
    {
        this.cells = new IslandCell[cells.Count];
        for (int i = 0; i < cells.Count; i++)
        {
            this.cells[i] = cells[i];
            cells[i].transform.parent = transform;
        }
        IsInited = true;
        OnIslandCreated?.Invoke(this);
    }
}
