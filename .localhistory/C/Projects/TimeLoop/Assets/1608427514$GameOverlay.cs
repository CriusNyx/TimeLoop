using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverlay : TimeBehaviour
{
    bool isPaused = false;
    Inventory playerInventory;
    

    // Start is called before the first frame update
    void Start()
    {
        RegisterAction(0f, 1f, RoutineToDo);
        transform.Find("PausePanel").gameObject.SetActive(false);
        playerInventory = GameObject.Find("Sphere").GetComponent<Inventory>();

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
        foreach (var (item, index) in collection.WithIndex())
        {
            Debug.WriteLine($"{index}: {item}");
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
