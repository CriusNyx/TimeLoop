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
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
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
