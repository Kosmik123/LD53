using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatsUIController : MonoBehaviour
{
    [Header("To Link")]
    [SerializeField]
    private PlayerStatsController playerStatsController;

    [Header("UI Elements")]
    [SerializeField]
    private TextMeshProUGUI pointsLabel;
    [SerializeField]
    private Slider fuelSlider;
    [SerializeField]
    private GameObject gameOverWindow;

    private void Awake()
    {
        fuelSlider.maxValue = 1;
        gameOverWindow.SetActive(false);
    }


    private void OnEnable()
    {
        playerStatsController.OnPointsChanged += RefreshPoints;
        playerStatsController.OnFuelEmpty += ShowGameOver;
    }

    private void RefreshPoints(int points)
    {
        pointsLabel.text = points.ToString();
    }

    private void ShowGameOver()
    {
        gameOverWindow.SetActive(true);
    }

    private void Update()
    {
        fuelSlider.value = playerStatsController.Fuel;
    }

    private void OnDisable()
    {
        playerStatsController.OnPointsChanged -= RefreshPoints;
        playerStatsController.OnFuelEmpty -= ShowGameOver;
    }
}
