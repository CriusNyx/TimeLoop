using UnityEngine;
using System.Collections;


/**
 * Used by player controller? UI grabs Inventory from PlayerController and does the render.
 */
public class Inventory
{
    public static int MAX_ITEMS = 10;

    ArrayList items = new ArrayList();

    void AddItem(Item item)
    {
        if(items.Count < MAX_ITEMS)
        {
            items.Add(item);
        }
    }
}
