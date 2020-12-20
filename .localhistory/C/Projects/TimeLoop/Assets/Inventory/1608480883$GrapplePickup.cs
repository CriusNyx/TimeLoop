using UnityEngine;
using System.Collections;

public class KeyItemPickup : Pickup
{
    public int code = 0;
    public Color color = Color.black;
    private void Start()
    {
        item = new GrappleItem();
    }
}
