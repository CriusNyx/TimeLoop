using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleItem : Item
{
    private Sprite _sprite;

    public GrappleItem()
    {
        _sprite = Resources.Load<Sprite>("grap");
    }

    public override Sprite GetSprite()
    {
        return _sprite;
    }

    // Do something if door is nearby? Check for door somehow?
    public override void Use()
    {
        GameObject.Find("Player").GetComponent<Player>().state.buffer.grappelHook = true;
    }

    public override void OnPickup()
    {
        throw new System.NotImplementedException();
    }
}
