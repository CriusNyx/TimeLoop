using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverlay : TimeBehaviour
{
    bool isPaused = false;
    Inventory playerInventory;
    GameObject[] slots = new GameObject[Inventory.MAX_ITEMS];
    

    // Start is called before the first frame update
    void Start()
    {
        playerInventory = GameObject.Find("Player").GetComponent<Inventory>();

        RegisterAction(0f, 1f, RoutineToDo);
        transform.Find("PausePanel").gameObject.SetActive(false);

        // Draw inventory slots
        for (var i = 0; i < Inventory.MAX_ITEMS; i++)
        {
            GameObject icon = new GameObject();
            icon.AddComponent<Image>();
            icon.AddComponent<Outline>();
            icon.transform.SetParent(transform.Find("StatusPanel").transform);
            icon.transform.Translate(new Vector3(200 + (i * 200), 200, 0));
            slots[i] = icon;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
            {
                SetPause(true);
            }
            else
            {
                SetPause(false);
            }
        }

        DrawInventory();
    }

    void DrawInventory()
    {
        Item[] items = playerInventory.GetItems();
        for(var i = 0; i < items.Length; i++)
        {
            // Set images
            if (items[i] != null)
            {
                slots[i].GetComponent<Image>().sprite = items[i].GetSprite();
            }else
            {
                slots[i].GetComponent<Image>().sprite = null;
            }
            if(i == playerInventory.GetSelectedIndex())
            {

            }
            else
            {
                slots[i].GetComponent<Outline>().effectDistance = new Vector2(5f, 5f);
            }
        }
    }

    void SetPause(bool status)
    {
        isPaused = status;
        Time.timeScale = isPaused ? 0f : 1f;
        transform.Find("PausePanel").gameObject.SetActive(isPaused);
        Cursor.visible = !isPaused;
        Cursor.lockState = isPaused ? CursorLockMode.None : CursorLockMode.Locked;
    }

    private IEnumerator RoutineToDo()
    {
        Debug.Log("I'm doing the thing.");
        // This behaviour will execute after a certain time once registered.
        yield return null;
    }

    protected override void ProtectedFixedUpdate()
    {

    }
}
