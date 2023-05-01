using UnityEngine;

public class ItemsDataAwaker : MonoBehaviour
{
    [SerializeField]
    private ItemsData itemsData;
    private void Awake()
    {
        itemsData.Awake();
    }
}
