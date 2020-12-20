using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : TimeBehaviour
{
    Inventory playerInventory;
    GameObject[] slots = new GameObject[Inventory.MAX_ITEMS];
    GameObject statusPanel;

    // Start is called before the first frame update
    void Start()
    {
        playerInventory = GameObject.Find("Player").GetComponent<Inventory>();
        statusPanel = transform.Find("StatusPanel").gameObject;

        // Draw inventory slots
        for (var i = 0; i < Inventory.MAX_ITEMS; i++)
        {
            GameObject icon = new GameObject();
            icon.AddComponent<Image>();
            icon.AddComponent<Outline>();
            icon.GetComponent<Outline>().effectColor = Color.red;
            icon.transform.SetParent(statusPanel.transform);
            icon.transform.Translate(new Vector3(150 + (i * 200), 150, 0));
            slots[i] = icon;
        }

    }

    // Update is called once per frame
    void Update()
    {
        DrawInventory();
    }

    void DrawInventory()
    {
        Item[] items = playerInventory.GetItems();
        for (var i = 0; i < items.Length; i++)
        {
            // Set images
            if (items[i] != null)
            {
                slots[i].GetComponent<Image>().sprite = items[i].GetSprite();
            }
            else
            {
                slots[i].GetComponent<Image>().sprite = null;
            }
            if (i == playerInventory.GetSelectedIndex())
            {
                slots[i].GetComponent<Outline>().effectDistance = new Vector2(5f, 5f);
            }
            else
            {
                slots[i].GetComponent<Outline>().effectDistance = Vector2.zero;
            }
        }
    }
    
    protected override void ProtectedFixedUpdate()
    {

    }
}
