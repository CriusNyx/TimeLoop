using UnityEngine;

public interface Item
{
    void Use();
    void OnPickup();
    Sprite GetSprite();
    string Name { get; }
}
