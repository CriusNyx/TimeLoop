using UnityEngine;
using System.Collections;

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
            Destroy(gameObject);
        }
    }
}
