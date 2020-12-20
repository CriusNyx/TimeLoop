using UnityEngine;

public abstract class Item
{
    public abstract void Use();
    public abstract void OnPickup();
    public abstract Sprite GetSprite();
    public string Name;
    public bool IsConsumable = false;
}
