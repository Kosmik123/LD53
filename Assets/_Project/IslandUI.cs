using UnityEngine;

public class IslandUI : MonoBehaviour
{
    [SerializeField]
    private Canvas canvas;
    [SerializeField]
    private ItemIcon itemIcon;
    [SerializeField]
    private IslandItemSource itemSource;
    [SerializeField]
    private ProgressLoader progressLoader;

    private void Awake()
    {
        canvas.worldCamera = Camera.main;
    }

    private void Update()
    {
        itemIcon.Progress = progressLoader.Progress;
    }

    private void Start()
    {
        itemIcon.Sprite = itemSource.Item.Icon;
    }
}
