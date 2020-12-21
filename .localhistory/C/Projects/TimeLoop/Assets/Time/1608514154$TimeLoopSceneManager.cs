using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeLoopSceneManager : MonoBehaviour
{
    public float timeRemaining = 10;

    private void Awake()
    {
    }

    public static TimeLoopSceneManager GetInstance()
    {
        return FindObjectOfType<TimeLoopSceneManager>();
    }
    public void TriggerDeath()
    {
        SceneManager.LoadScene("TimeLoop");
    }

    private void Start()
    {

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            TriggerDeath();
        }

        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;

            float minutes = Mathf.FloorToInt((timeRemaining + 1) / 60);
            float seconds = Mathf.FloorToInt((timeRemaining + 1) % 60);
            
            GameObject.Find("HUD").GetComponent<GameOverlay>().SetCountdownText(string.Format("{0:00}:{1:00}", minutes, seconds));
        }
        else
        {
            TriggerDeath();
            timeRemaining = 0;
        }
    }
}