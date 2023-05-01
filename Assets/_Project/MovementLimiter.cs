using UnityEngine;

public class MovementLimiter : MonoBehaviour
{
    [Header("To Link")]
    [SerializeField]
    private BoxCollider2D topCollider;
    [SerializeField]
    private BoxCollider2D leftCollider;
    [SerializeField]
    private BoxCollider2D rightCollider;
    [SerializeField]
    private BoxCollider2D bottomCollider;

    [Header("Properties")]
    [SerializeField]
    private Vector2 limitingSize;
    public Vector2 LimitingSize
    {
        get => limitingSize;
        set
        {
            limitingSize = value;
        }
    }

    private void OnEnable()
    {
        EnableColliders();
    }

    private void EnableColliders(bool active = true)
    {
        topCollider.enabled = active;
        leftCollider.enabled = active;
        rightCollider.enabled = active;
        bottomCollider.enabled = active;
    }

    private void Update()
    {
        UpdateLimits();
    }

    private void UpdateLimits()
    {
        topCollider.size = bottomCollider.size = new Vector2(limitingSize.x + 2, 1);
        leftCollider.size = rightCollider.size = new Vector2(1, limitingSize.y + 2);

        leftCollider.transform.localPosition = new Vector3(-0.5f * (limitingSize.x + 1), 0);
        rightCollider.transform.localPosition = new Vector3(0.5f * (limitingSize.x + 1), 0);
        topCollider.transform.localPosition = new Vector3(0, 0.5f * (limitingSize.y + 1));
        bottomCollider.transform.localPosition = new Vector3(0, -0.5f * (limitingSize.y + 1));
    }

    private void OnDisable()
    {
        EnableColliders(false);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, limitingSize);
    }
}
