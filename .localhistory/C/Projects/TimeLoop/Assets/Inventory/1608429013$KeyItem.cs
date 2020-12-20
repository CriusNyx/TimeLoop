using UnityEngine;
using System.Collections;

public class KeyItem : Item
{
    private string _name = "key";
    private int _code;
    private Color _color;
    private Sprite _sprite;

    public string Name { get => "name"; set => _name = value; }

    public KeyItem(int code, Color color)
    {
        _code = code;
        _color = color;
        _sprite = Resources.Load<Sprite>("sprit.png");
    }

    public Sprite GetSprite()
    {
        // TODO: Add color to sprite
        return _sprite;
    }

    public void OnPickup()
    {
        throw new System.NotImplementedException();
    }

    // Do something if door is nearby? Check for door somehow?
    public void Use()
    {
        Debug.Log("use");
        GameObject.Find("Sphere").GetComponent<Rigidbody>().AddForce(new Vector3(0, 1000, 0));
    }
}
