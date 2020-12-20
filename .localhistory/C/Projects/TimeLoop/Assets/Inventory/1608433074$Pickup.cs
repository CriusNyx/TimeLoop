﻿using UnityEngine;
using System.Collections;

/**
 * Apply this bad boy to a gameobject and set the 'item' field to create the pickup for that item.
 */
public abstract class Pickup : MonoBehaviour
{
    protected abstract Item item {get; set;}

    protected BoxCollider pickupCollider;

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

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        OnPickup(other);
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
