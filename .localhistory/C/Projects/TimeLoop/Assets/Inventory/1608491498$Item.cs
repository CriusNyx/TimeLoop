using UnityEngine;

public abstract class Item
{
    protected abstract void Use();
    protected abstract void OnPickup();
    protected abstract Sprite GetSprite();
    string Name;
    bool IsConsumable;
}
