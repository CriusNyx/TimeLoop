using UnityEngine;
using System.Collections;

public class KeyItem : Item
{
    private int _code;
    private Color _color;
    private Sprite _sprite;

    public string Name = "Key";
    public bool IsConsumable = true;

    public KeyItem(int code, Color color)
    {
        _code = code;
        _color = color;
        _sprite = Resources.Load<Sprite>("key");
    }

    public override Sprite GetSprite()
    {
        // TODO: Add color to sprite
        return _sprite;
    }

    public override void OnPickup()
    {
        throw new System.NotImplementedException();
    }
    
    public override void Use()
    {
        Collider[] colliders = Physics.OverlapSphere(GameObject.Find("Player").transform.position, 2f);
        foreach (var collider in colliders)
        {
            LockedDoor door = collider.gameObject.GetComponent<LockedDoor>();
            if(door != null && door.locked && door.code == _code)
            {
                door.Open();
            }
        }
    }
}
