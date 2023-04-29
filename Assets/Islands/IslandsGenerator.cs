using UnityEngine;
using NaughtyAttributes;
using System.Collections.Generic;
using UnityEditor;

public class IslandsGenerator : MonoBehaviour
{
    [SerializeField]
    private IslandSettings settings;
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
        Island island = PrefabUtility.InstantiatePrefab(islandTemplate, transform) as Island; //Instantiate(islandTemplate, Random.insideUnitCircle * 10, Quaternion.identity, transform);
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

        var cell = PrefabUtility.InstantiatePrefab(islandCellTemplate, island) as IslandCell; //Instantiate(islandCellTemplate, island);
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
