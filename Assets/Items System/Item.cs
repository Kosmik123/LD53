using System;
using System.Collections;
using UnityEngine;

[CreateAssetMenu]
public class Item : ScriptableObject
{
    [SerializeField]
    private Sprite icon;
    public Sprite Icon => icon;
}
