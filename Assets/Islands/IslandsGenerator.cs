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

    public Island GenerateIsland(Vector3 position)
    {
        tempCells.Clear();
        Island island = spawningStrategy.Spawn(islandTemplate, transform);
        island.transform.localPosition = position;
        int cellsCount = Random.Range(settings.MinCellsCount, settings.MaxCellsCount);
        for (int i = 0; i < cellsCount; i++)
        {
            var cell = GenerateCell(island.transform);
            tempCells.Add(cell);
        }
        island.Init(tempCells);
        return island;
    }

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
        GenerateIsland(Random.insideUnitCircle * 20);
    }

    private IslandCell GenerateCell(Transform island)
    {
        Vector2 position = Random.insideUnitCircle;
        float beachRadius = Random.Range(settings.MinCellRadius, settings.MaxCellRadius);
        float grassRadius = beachRadius * Random.Range(settings.MinCellGrassRelativeRadius, settings.MaxCellGrassRelativeRadius);

        var cell = spawningStrategy.Spawn(islandCellTemplate, island);
        cell.transform.localPosition = position;

        var grassData = cell.Grass;
        grassData.Radius = grassRadius;
        cell.Grass = grassData;

        var beachData = cell.Beach;
        beachData.Radius = beachRadius;
        cell.Beach = beachData;

        var lagunaData = cell.Laguna;
        lagunaData.Radius = beachRadius * 2;
        cell.Laguna = lagunaData;

        return cell;
    }
}
