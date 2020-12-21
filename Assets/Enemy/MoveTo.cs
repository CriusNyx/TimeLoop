using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveTo : MonoBehaviour
{
    public PathNode nextDestination;
    public int startDelay;
    Transform t;
    public bool fixedRotation = false;
    public float rotationIfFixed = 90f;
    public bool delayFinished = false;

    void Start()
    {
        t = transform;
        if (nextDestination != null)
        {
            NavMeshAgent agent = GetComponent<NavMeshAgent>();
            StartCoroutine(Delay(agent));
        } else
        {
            Destroy(GetComponent<NavMeshAgent>());
        }
    }
    void Update()
    {
        if (fixedRotation == true && delayFinished == true)
        {
            t.eulerAngles = new Vector3(t.eulerAngles.x, rotationIfFixed, t.eulerAngles.z);
        }
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
        delayFinished = true;
    }

}
