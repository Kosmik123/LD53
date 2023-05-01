using System.Collections.Generic;
using UnityEngine;

public class IslandsManager : MonoBehaviour
{
    [SerializeField]
    private IslandsGenerator islandsGenerator;

    [SerializeField]
    private Vector2 chunkSize;

    private Dictionary<Vector2Int, Island> islands;

    private void Awake()
    {
        InitializeDictionaryWithExistentIslands();
    }

    private void InitializeDictionaryWithExistentIslands()
    {
        var existingIslands = FindObjectsOfType<Island>();
        islands = new Dictionary<Vector2Int, Island>(existingIslands.Length);
        for (int i = 0; i < existingIslands.Length; i++)
        {
            var island = existingIslands[i];
            Vector2Int position = WorldToGrid(island.transform.position);
            islands.Add(position, island);
        }
    }

    public void GenerateIsland(Vector2Int position)
    {
        Vector2 center = GridToWorld(position);

        Vector2 islandPosition = new Vector2(
            Random.Range(center.x - 0.5f * chunkSize.x, center.x + 0.5f * chunkSize.x),
            Random.Range(center.y - 0.5f * chunkSize.y, center.y + 0.5f * chunkSize.y));

        var island = islandsGenerator.GenerateIsland(islandPosition);
        islands.Add(position, island);
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
