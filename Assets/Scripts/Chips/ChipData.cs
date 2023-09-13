using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ChipData : ScriptableObject
{
    public string id = "000";
    public string displayName = "New Ability";
    public Sprite icon;
    public GameObject prefab;

    public int Damage = 0;

    public ElementType Element = ElementType.None;
    public ChipEffect ChipEffect = ChipEffect.Cannon;

    // If 0,0 use on self
    // if 10,10 hitscan/raycast
    // Hopefully a tuple
    public List<GridCoordinates> TargetGridCoordinates = new List<GridCoordinates>();
    // Also Enum this for card rankings
    public string Code = "F";

    public abstract void Initialize(GameObject obj);
    public abstract void TriggerAbility(Transform userTransform);

    public virtual void OnUse(Transform userTransform)
    {
        Debug.Log("derived name: " + displayName);
    }
}

public enum ElementType
{
    None,
    Fire,
    Water,
    Electric,
    Earth,
    Air,
    Dark,
}

public enum ChipEffect
{
    Spread,
    Shotgun,
    Shockwave,
    Cannon,
    Self,
    Target,
    Field,
    Trap,
    Sensor,
    Sword,
    Champion
}