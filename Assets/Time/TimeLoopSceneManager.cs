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

    private void Start()
    {

    }

    private void Update()
    {

    }
}