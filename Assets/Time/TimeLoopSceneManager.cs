using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeLoopSceneManager : MonoBehaviour
{
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
    }
}