using UnityEngine;
using System.Collections;

public class KeyItem : Item
{
    public string Name { get => "name"; }

    public Sprite GetSprite()
    {
        return Resources.Load<Texture2D>("sprit");
    }

    public void OnPickup()
    {
        throw new System.NotImplementedException();
    }

    public void Use()
    {
        throw new System.NotImplementedException();
    }

}
