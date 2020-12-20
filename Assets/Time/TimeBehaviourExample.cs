using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBehaviourExample : TimeBehaviour
{
    private void Start()
    {
        // Routine to do will execute in 0 minutes and 1 second.
        RegisterAction(Time.timeSinceLevelLoad + 10f, RoutineToDo);
    }

    private IEnumerator RoutineToDo()
    {
        Debug.Log("I'm doing the thing.");
        // This behaviour will execute after a certain time once registered.

        yield return null;
    }

    protected override void ProtectedFixedUpdate()
    {

    }
}