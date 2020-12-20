using UnityEngine;
using System.Collections.Generic;

/**
 * Used by player controller? UI grabs Inventory from PlayerController and does the render.
 */
public class Inventory : TimeBehaviour
{
    public static int MAX_ITEMS = 10;

    List<Item> items = new List<Item>();

    int currentIndex = 0;

    public void AddItem(Item item)
    {
        Debug.Log(item.Name);
        if(items.Count < MAX_ITEMS)
        {
            items.Add(item);
        }
    }

    public Item GetItem(int index)
    {
        return items[index];
    }

    public Item GetSelectedItem()
    {
        return items[currentIndex];
    }

    public void SelectItem(int index)
    {
        currentIndex = Mathf.Min(10, Mathf.Max(0, index));
    }

    public void UseSelectedItem()
    {
        GetSelectedItem().Use();
    }




    protected override void ProtectedFixedUpdate()
    {

    }
}
