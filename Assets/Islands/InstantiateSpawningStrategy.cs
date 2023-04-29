using UnityEngine;

public class InstantiateSpawningStrategy : SpawningStrategy
{
    public override T Spawn<T>(T template)
    {
        return Instantiate(template);
    }

    public override T Spawn<T>(T template, Transform parent)
    {
        return Instantiate(template, parent);
    }
}
