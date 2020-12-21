using UnityEngine;

public abstract class Item
{
    /**
     * Return true on successful use of item
     */
    public abstract bool Use();
    public abstract void OnPickup();
    public abstract Sprite GetSprite();
    public string Name = "NAME NOT SET";
    public bool IsConsumable = false;
}
