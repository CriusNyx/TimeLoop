using UnityEngine;
using System.Collections;

public class GrapplePickup : Pickup
{
    public int code = 0;
    public Color color = Color.black;
    private void Start()
    {
        item = new GrappleItem();
    }
}
