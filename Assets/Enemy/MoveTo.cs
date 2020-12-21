using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveTo : MonoBehaviour
{
    public PathNode nextDestination;
    public int startDelay;


    void Start()
    {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        StartCoroutine(Delay(agent));
    }
    IEnumerator Patrol(NavMeshAgent agent)
    {
        if (nextDestination?.next != null)
        {
            agent.destination = nextDestination.transform.position;
            nextDestination = nextDestination.next;
            yield return new WaitForSeconds(3);
            StartCoroutine(Patrol(agent));
        }

    }
    IEnumerator Delay(NavMeshAgent agent)
    {
        yield return new WaitForSeconds(startDelay);
        StartCoroutine(Patrol(agent));
    }

}
