using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStatsController : MonoBehaviour
{
    public event System.Action<int> OnPointsChanged;
    public event System.Action OnFuelEmpty;

    [SerializeField]
    private MonoBehaviour[] componentsToDisableAtGameover;

    [SerializeField, ReadOnly]
    private int points;
    public int Points => points;

    [SerializeField, Range(0,1)]
    private float fuel;
    public float Fuel => fuel;

    [SerializeField, Range(0,1)]
    private float fuelLoweringSpeed;
    public float FuelingLoweringSpeed
    {
        get => fuelLoweringSpeed;
        set
        {
            fuelLoweringSpeed = value;
        }
    }

    [SerializeField, Range(0, 1)]
    private float fuelHealingValue;

    [SerializeField, ReadOnly]
    private List<IslandItemDemandController> islands;

    public void AddIsland(Island island)
    {
        if (island.TryGetComponent<IslandItemDemandController>(out var islandDemandController))
        {
            islands.Add(islandDemandController);
            islandDemandController.OnItemCollected += AddPoint;
        }
    }

    private void Update()
    {
        fuel -= Time.deltaTime * fuelLoweringSpeed;
        if (fuel <= 0)
        {
            OnFuelEmpty?.Invoke();
            foreach (var component in componentsToDisableAtGameover)
                component.enabled = false;
        }
        fuel = Mathf.Clamp01(fuel);
    }

    private void AddPoint(Item item)
    {
        if (item)
        {
            points++;
            fuel += fuelHealingValue;
            fuelLoweringSpeed *= 1.08f;
            OnPointsChanged?.Invoke(points);
        }
    }

    public void ResetLevel()
    {
        SceneManager.LoadSceneAsync(0);
    }
}
