using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverlay : TimeBehaviour
{
    bool isPaused = false;
    Inventory playerInventory;
    GameObject[] slots;
    

    // Start is called before the first frame update
    void Start()
    {
        RegisterAction(0f, 1f, RoutineToDo);
        transform.Find("PausePanel").gameObject.SetActive(false);
        playerInventory = GameObject.Find("Sphere").GetComponent<Inventory>();

        for (var i = 0; i < Inventory.MAX_ITEMS; i++)
        {
            GameObject icon = new GameObject();
            icon.AddComponent<Image>();
            icon.transform.SetParent(transform.Find("StatusPanel").transform);
            icon.transform.Translate(new Vector3(i * 200, 200, 0));
            slots[0] = icon;
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
        }
    }

    void SetPause(bool status)
    {
        isPaused = status;
        Time.timeScale = isPaused ? 0f : 1f;
        transform.Find("PausePanel").gameObject.SetActive(isPaused);
        Cursor.visible = !isPaused;
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
