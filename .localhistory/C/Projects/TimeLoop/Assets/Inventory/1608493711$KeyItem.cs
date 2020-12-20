using UnityEngine;
using System.Collections;

public class KeyItem : Item
{
    private int _code;
    private Color _color;
    private Sprite _sprite;

    public KeyItem(int code, Color color)
    {
        _code = code;
        _color = color;
        _sprite = Resources.Load<Sprite>("key");
        Name = "Key";
        IsConsumable = true;
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
    
    public override bool Use()
    {
        Debug.Log("Use key");
        Collider[] colliders = Physics.OverlapSphere(GameObject.Find("Player").transform.position, 2f);
        foreach (var collider in colliders)
        {
            LockedDoor door = collider.gameObject.GetComponent<LockedDoor>();
            if(door != null && door.locked && door.code == _code)
            {
                door.Open();
                return true;
            }
        }

        return false;
    }
}
