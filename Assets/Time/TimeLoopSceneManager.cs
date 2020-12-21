using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeLoopSceneManager : MonoBehaviour
{
    float timeRemaining = 60 * 5;

    public Animator playerAnimator;

    private void Awake()
    {
    }

    public static TimeLoopSceneManager GetInstance()
    {
        return FindObjectOfType<TimeLoopSceneManager>();
    }
    public IEnumerator TriggerDeath()
    {
        playerAnimator.SetTrigger("doDeath");
        // TODO: add some on death effect
        yield return new WaitForSeconds(0.5f);
        Restart();
    }

    public void Restart()
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
            StartCoroutine(TriggerDeath());
        }

        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;

            float minutes = Mathf.FloorToInt((timeRemaining + 1) / 60);
            float seconds = Mathf.FloorToInt((timeRemaining + 1) % 60);
            GameObject.Find("HUD").GetComponent<GameOverlay>().SetCountdownText(string.Format("{0:0}:{1:00}", minutes, seconds));
        }
        else
        {
            StartCoroutine(TriggerDeath());
            timeRemaining = 0;
        }
    }
}