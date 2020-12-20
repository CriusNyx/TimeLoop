using UnityEngine;
using System.Collections.Generic;

/**
 * Used by player controller? UI grabs Inventory from PlayerController and does the render.
 */
public class Inventory
{
    public static int MAX_ITEMS = 10;

    List<Item> items = new List<Item>();

    int currentIndex;

    void AddItem(Item item)
    {
        if(items.Count < MAX_ITEMS)
        {
            items.Add(item);
        }
    }

    Item GetItem(int index)
    {
        return items[index];
    }

    void SelectIndex(int index)
    {
        currentIndex = index;
    }
}
