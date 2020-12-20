using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedDoor : MonoBehaviour
{
    public int code;
    // Start is called before the first frame update
    void Start()
    {
        //pickupCollider = gameObject.AddComponent<BoxCollider>();
        //pickupCollider.size = Vector3.one * 2;
        //pickupCollider.isTrigger = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Inventory inventory = other.gameObject.GetComponentInParent<Inventory>();
        if (inventory != null)
        {

        }

    }
    public void Open()
    {
        Debug.Log("I HAVE BEEN UNLOCKED AH");
        // move up lol
    }
}
