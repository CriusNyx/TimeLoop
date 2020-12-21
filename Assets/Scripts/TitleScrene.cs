using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScrene : MonoBehaviour
{
    public GameObject winView;

    public void Start()
    {
        winView.SetActive(PlayerPowerupState.isWin);  
    }

    public void StartGame()
    {
        PlayerPowerupState.ResetAllPowerups();
        SceneManager.LoadScene("TimeLoop");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
