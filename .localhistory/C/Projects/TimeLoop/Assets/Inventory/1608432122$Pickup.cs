using UnityEngine;
using System.Collections;

/**
 * Apply this bad boy to a gameobject and set the 'item' field for fun
 */ 
public class Pickup : MonoBehaviour
{

    protected BoxCollider pickupCollider;

    public Item item;

    private void Awake()
    {
        pickupCollider = gameObject.AddComponent<BoxCollider>();
        pickupCollider.size = Vector3.one * 2;
        pickupCollider.isTrigger = true;
    }

    // Use this for initialization
    void Start()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        OnPickup(other);
    }

    // Update is called once per frame
    void Update()
    {

    }

    protected virtual void OnPickup(Collider other)
    {
        Inventory inventory = other.gameObject.GetComponentInParent<Inventory>();
        if (inventory != null)
        {
            inventory.AddItem(item);
            Debug.Log("Picked up item!~");
            Destroy(gameObject);
        }
    }
}
