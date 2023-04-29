using UnityEngine;

public interface ITransform
{
    public Vector3 Position { get; set; }
    public Quaternion Rotation { get; set; }
}

public class TransformAdapter : MonoBehaviour, ITransform
{
    public Vector3 Position 
    {
        get => transform.position;
        set => transform.position = value;
    }

    public Quaternion Rotation
    {
        get => transform.rotation;
        set => transform.rotation = value;
    }
}
