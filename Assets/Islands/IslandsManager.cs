using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;

public class IslandsManager : MonoBehaviour
{
    [SerializeField]
    private IslandsGenerator islandsGenerator;
    [SerializeField]
    private Vector2 chunkSize;
    [SerializeField]
    private Transform observer;
    [SerializeField]
    private Camera viewCamera;
    [SerializeField, Range(0,1)]
    private float spawnIslandProbability;
    [SerializeField]
    private PlayerStatsController playerStatsController;

    private Dictionary<Vector2Int, Island> islandsByPosition;

    [SerializeField, ReadOnly]
    private readonly List<Vector2Int> positionsVisibleLastFrame = new List<Vector2Int>();

    private void Awake()
    {
        InitializeDictionaryWithExistentIslands();
    }

    private void InitializeDictionaryWithExistentIslands()
    {
        var existingIslands = FindObjectsOfType<Island>();
        islandsByPosition = new Dictionary<Vector2Int, Island>(existingIslands.Length);
        for (int i = 0; i < existingIslands.Length; i++)
        {
            var island = existingIslands[i];
            Vector2Int position = WorldToGrid(island.transform.position);
            islandsByPosition.Add(position, island);
            playerStatsController.AddIsland(island);
        }
    }

    private void Start()
    {
        const int yStart = -1;
        const int yEnd = 1;
        const int xStart = -1;
        const int xEnd = 2;
        
        for (int j = yStart; j <= yEnd; j++)
        {
            for (int i = xStart; i <= xEnd; i++)
            {
                var position = new Vector2Int(i, j);
                if (islandsByPosition.ContainsKey(position) == false)
                    islandsByPosition.Add(position, null);
            }
        }
    }

    private void Update()
    {
        foreach (var position in positionsVisibleLastFrame)
            if (islandsByPosition.TryGetValue(position, out var island))
                if (island != null)
                    island.IsVisible = false;

        positionsVisibleLastFrame.Clear();

        float yExtent = viewCamera.orthographicSize;
        float xExtent = yExtent * viewCamera.aspect;
        Vector2 observerPosition = observer.position;

        Vector2Int start = WorldToGrid(new Vector2(
            Mathf.Floor(observerPosition.x - xExtent),
            Mathf.Floor(observerPosition.y - yExtent)));

        Vector2Int end = WorldToGrid(new Vector2(
            Mathf.Ceil(observerPosition.x + xExtent),
            Mathf.Ceil(observerPosition.y + yExtent)));

        for (int j = start.y - 1; j <= end.y + 1; j++)
        {
            for (int i = start.x - 1; i <= end.x + 1; i++)
            {
                var position = new Vector2Int(i, j);
                positionsVisibleLastFrame.Add(position);
                if (islandsByPosition.TryGetValue(position, out var island))
                {
                    if (island != null)
                        island.IsVisible = true;
                }
                else
                {
                    if (Random.value < spawnIslandProbability)
                        GenerateIsland(position);
                    else
                        islandsByPosition.Add(position, null);
                }
            }
        }
    }

    public void GenerateIsland(Vector2Int position)
    {
        Vector2 center = GridToWorld(position);

        Vector2 islandPosition = new Vector2(
            Random.Range(center.x - 0.5f * chunkSize.x, center.x + 0.5f * chunkSize.x),
            Random.Range(center.y - 0.5f * chunkSize.y, center.y + 0.5f * chunkSize.y));

        var island = islandsGenerator.GenerateIsland(islandPosition);
        islandsByPosition.Add(position, island);
        playerStatsController.AddIsland(island);
    }

    public Vector2Int WorldToGrid(Vector3 position)
    {
        int x = Mathf.RoundToInt(position.x / chunkSize.x);
        int y = Mathf.RoundToInt(position.y / chunkSize.y);
        return new Vector2Int(x, y);
    }

    public Vector2 GridToWorld(Vector2Int position)
    {
        Vector2 worldPosition = position;
        worldPosition.Scale(chunkSize);
        return worldPosition;
    }
}
