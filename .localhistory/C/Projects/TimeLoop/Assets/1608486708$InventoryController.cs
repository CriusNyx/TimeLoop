using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : TimeBehaviour
{
    Inventory playerInventory;
    GameObject[] slots = new GameObject[Inventory.MAX_ITEMS];
    GameObject statusPanel;

    private void Awake()
    {
        statusPanel = transform.Find("StatusPanel").gameObject;

        // Draw inventory slots
        for (var i = 0; i < Inventory.MAX_ITEMS; i++)
        {
            GameObject icon = new GameObject();
            icon.AddComponent<Image>();
            icon.AddComponent<Outline>();
            icon.GetComponent<Outline>().effectColor = Color.red;
            icon.transform.SetParent(statusPanel.transform);
            //Debug.Log(GetComponent<RectTransform>().rect.width / 2);
            icon.transform.Translate(new Vector3((i * 200), 0, 0));
            slots[i] = icon;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        playerInventory = GameObject.Find("Player").GetComponent<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {
        if( Input.mouseScrollDelta.y < 0)
        {
            playerInventory.SelectNextItem();
        }else if (Input.mouseScrollDelta.y > 0)
        {
            playerInventory.SelectPreviousItem();
        }


        // Crappy number selection lol
        for(int i = 48; i < 58; i++)
        {
            if (Input.GetKeyDown((KeyCode) i))
            {
                if(i == 48)
                {
                    playerInventory.SelectItem(9);
                }
                else
                {
                    playerInventory.SelectItem(i - 49);
                }
            }
        }


        DrawInventory();
    }

    void DrawInventory()
    {
        Item[] items = playerInventory.GetItems();
        for (var i = 0; i < items.Length; i++)
        {

            slots[i].GetComponent<Image>().sprite = null;
            slots[i].GetComponent<Outline>().effectDistance = Vector2.zero;

            // Set images
            if (items[i] != null)
            {
                slots[i].GetComponent<Image>().sprite = items[i].GetSprite();
            }
            
            if (i == playerInventory.GetSelectedIndex())
            {
                slots[i].GetComponent<Outline>().effectDistance = new Vector2(5f, 5f);
            }
        }
    }
    
    protected override void ProtectedFixedUpdate()
    {

    }
}
