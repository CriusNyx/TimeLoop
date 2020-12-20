﻿using UnityEngine;
using System.Collections;

public class KeyItem : Item
{
    private string _name = "key";
    private int _code;
    private Color _color;

    public string Name { get => "name"; set => _name = value; }



    public KeyItem(int code, Color color)
    {
        _code = code;
        _color = color;
    }

    public Sprite GetSprite()
    {
        // TODO: Add color to sprite
        return Sprite.Create(Resources.Load<Texture2D>("sprit"), new Rect(0, 0, 100, 100), Vector2.zero);
    }

    public void OnPickup()
    {
        throw new System.NotImplementedException();
    }

    public void Use()
    {
        GameObject.Find("Player");
    }
}
