using UnityEngine;
using NaughtyAttributes;
using System.Collections.Generic;
using UnityEditor;

public abstract class SpawningStrategy : MonoBehaviour
{
    public abstract T Spawn<T>(T template) where T : Object;
    public abstract T Spawn<T>(T template, Transform parent) where T : Object;
}

public class IslandsGenerator : MonoBehaviour
{
    [SerializeField]
    private IslandSettings settings;
    [SerializeField]
    private SpawningStrategy spawningStrategy;
    [SerializeField]
    private IslandCell islandCellTemplate;
    [SerializeField]
    private Island islandTemplate;

    private List<IslandCell> tempCells = new List<IslandCell>();

    [Button]
    private void Clear()
    {
        var allIslands = GetComponentsInChildren<Island>();
        foreach (var island in allIslands)
            DestroyImmediate(island.gameObject);
    }

    [Button]
    private void Generate()
    {
        tempCells.Clear();
        Island island = spawningStrategy.Spawn(islandTemplate, transform);
        island.transform.localPosition = Random.insideUnitCircle * 20;
        int cellsCount = Random.Range(settings.MinCellsCount, settings.MaxCellsCount);
        for (int i = 0; i < cellsCount; i++)
        {
            var cell = GenerateCell(island.transform);
            tempCells.Add(cell);
        }
        island.Init(tempCells);
    }

    private IslandCell GenerateCell(Transform island)
    {
        Vector2 position = Random.insideUnitCircle;
        float beachRadius = Random.Range(settings.MinCellRadius, settings.MaxCellRadius);
        float grassRadius = beachRadius * Random.Range(settings.MinCellGrassRelativeRadius, settings.MaxCellGrassRelativeRadius);

        var cell = spawningStrategy.Spawn(islandCellTemplate, island);
        cell.transform.localPosition = position;
        var beachData = cell.Beach;
        beachData.Radius = beachRadius;
        cell.Beach = beachData;
        var grassData = cell.Grass;
        grassData.Radius = grassRadius;
        cell.Grass = grassData;
        
        return cell;
    }
}
