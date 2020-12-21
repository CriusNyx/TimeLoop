using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScrene : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("TimeLoop");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
