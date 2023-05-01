using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;

public class Island : MonoBehaviour
{
    public event System.Action<Island> OnIslandCreated;
    public event System.Action<bool> OnVisibilityChanged;


    [SerializeField]
    private IslandCell[] cells;
    public IReadOnlyList<IslandCell> Cells => cells;

    [field: ShowNonSerializedField, ReadOnly]
    public bool IsInited { get; private set; } = false;

    [ShowNonSerializedField, ReadOnly]
    private bool isVisible;
    public bool IsVisible
    {
        get => isVisible;
        set
        {
            if (isVisible != value)
            {
                isVisible = value;
                if (isVisible && gameObject.activeSelf == false)
                {
                    gameObject.SetActive(true);
                    OnVisibilityChanged?.Invoke(true);
                }
            }
        }
    }


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

    private void LateUpdate()
    {
        if (isVisible == false)
        {
            gameObject.SetActive(false);
            OnVisibilityChanged?.Invoke(false);
        }
    }
}
