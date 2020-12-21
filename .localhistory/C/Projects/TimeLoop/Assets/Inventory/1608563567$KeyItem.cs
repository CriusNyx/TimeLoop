using UnityEngine;
using System.Collections;

public class KeyItem : Item
{
    private int _code = 0;
    private Sprite _sprite;

    public KeyItem()
    {
        _sprite = Resources.Load<Sprite>("key");
        Name = "Key";
        IsConsumable = true;
    }

    public override Sprite GetSprite()
    {
        return _sprite;
    }

    public override void OnPickup()
    {
        throw new System.NotImplementedException();
    }
    
    public override bool Use()
    {
        Collider[] colliders = Physics.OverlapSphere(GameObject.Find("Player").transform.position, 5f);
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
