using UnityEngine;
using System.Collections;

public class GrapplePickup : Pickup
{
    private void Start()
    {
        item = new GrappleItem();
    }
}
