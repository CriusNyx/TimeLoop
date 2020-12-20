using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AirEnemy : MonoBehaviour
{
    public PathNode nextDestination;
    public float speed = 10f;
    public int startDelay;
    public bool delayFinished = false;

    void Start()
    {
        StartCoroutine(Delay());
    }

    // Update is called once per frame
    void Update()
    {
        if (nextDestination != null)
        {
            if (delayFinished == true && transform.position == nextDestination.transform.position)
            {
                delayFinished = false;
                StartCoroutine(PauseMovement());

            }
            else if (delayFinished == true && transform.position != nextDestination.transform.position)
            {
                transform.position = Vector3.MoveTowards(transform.position, nextDestination.transform.position, speed * Time.deltaTime);
            }
        }
    }
    IEnumerator Delay()
    {
        yield return new WaitForSeconds(startDelay);
        delayFinished = true;
    }
    IEnumerator PauseMovement()
    {
        yield return new WaitForSeconds(4);
        nextDestination = nextDestination.next;
        delayFinished = true;
    }
}
