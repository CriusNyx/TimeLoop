using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverlay : TimeBehaviour
{
    bool isPaused = false;
    

    // Start is called before the first frame update
    void Start()
    {
        transform.Find("PausePanel").gameObject.SetActive(false);
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

    }

    public void ResetGame()
    {

    }

    public void SetCountdownText(string text)
    {
        transform.Find("StatusPanel/CountdownText").GetComponent<Text>().text = text;
    }

    void SetPause(bool status)
    {
        isPaused = status;
        Time.timeScale = isPaused ? 0f : 1f;
        transform.Find("PausePanel").gameObject.SetActive(isPaused);
        Cursor.visible = !isPaused;
        Cursor.lockState = isPaused ? CursorLockMode.None : CursorLockMode.Locked;
    }

    protected override void ProtectedFixedUpdate()
    {

    }
}
