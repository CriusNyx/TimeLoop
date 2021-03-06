﻿using UnityEngine;
using System.Collections;

public class Pickup : MonoBehaviour
{

    protected new BoxCollider pickupCollider;

    // Use this for initialization
    void Start()
    {
        pickupCollider = gameObject.AddComponent<BoxCollider>();
        pickupCollider.size = Vector3.one * 2;
        pickupCollider.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        OnPickup(other);
    }

    // Update is called once per frame
    void Update()
    {

    }

    protected virtual void OnPickup(Collider other) { }
}
