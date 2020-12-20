using UnityEngine;
using System.Collections.Generic;

/**
 * Used by player controller? UI grabs Inventory from PlayerController and does the render.
 */
public class Inventory : TimeBehaviour
{
    public static int MAX_ITEMS = 10;

    Item[] items = new Item[MAX_ITEMS];

    int currentIndex = 0;

    public void AddItem(Item item)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] == null)
            {
                items[i] = item;
                break;
            }
        }
    }

    // TODO: make more restricted
    public Item[] GetItems()
    {
        return items;
    }

    public Item GetItem(int index)
    {
        return items[index];
    }

    public Item GetSelectedItem()
    {
        return items[currentIndex];
    }

    public int GetSelectedIndex()
    {
        return currentIndex;
    }

    public void SelectItem(int index)
    {
        currentIndex = Mathf.Min(MAX_ITEMS - 1, Mathf.Max(0, index));
    }

    public void UseSelectedItem()
    {
        GetSelectedItem()?.Use();
    }

    public void SelectNextItem()
    {
        currentIndex++;
        if (currentIndex > MAX_ITEMS - 1)
        {
            currentIndex = 0;
        }
    }

    public void SelectPreviousItem()
    {
        currentIndex--;
        if(currentIndex < 0)
        {
            currentIndex = MAX_ITEMS - 1;
        }
    }



    protected override void ProtectedFixedUpdate()
    {

    }
}
