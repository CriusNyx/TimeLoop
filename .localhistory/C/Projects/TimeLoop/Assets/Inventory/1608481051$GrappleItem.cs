using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleItem : Item
{
    private Sprite _sprite;

    public string Name { get; set; } = "Grapple hook";

    public GrappleItem()
    {
        _sprite = Resources.Load<Sprite>("sprit");
    }

    public Sprite GetSprite()
    {
        return _sprite;
    }

    public void OnPickup()
    {
        throw new System.NotImplementedException();
    }

    // Do something if door is nearby? Check for door somehow?
    public void Use()
    {
        GameObject.Find("Player").GetComponent<Player>().state.buffer.grappelHook = true;
    }
}
