using UnityEngine;
using System.Collections;

/*
 * Idk a nicer way to do this... I would like to avoid creating these classes and just setting stuff in the inspector but whatever
 */
public class KeyItemPickup : Pickup
{
    public int code = 0;
    public Color color = Color.black;
    private void Start()
    {
        item = new KeyItem();
    }
}
