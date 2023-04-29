using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR
public class InstantiatePrefabSpawningStrategy : SpawningStrategy
{
    public override T Spawn<T>(T template)
    {
        return PrefabUtility.InstantiatePrefab(template) as T;
    }

    public override T Spawn<T>(T template, Transform parent)
    {
        return PrefabUtility.InstantiatePrefab(template, parent) as T;
    }
}
#endif