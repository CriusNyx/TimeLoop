using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TimeBehaviour : MonoBehaviour
{
    private List<(float time, Func<IEnumerator> func)> actions = new List<(float time, Func<IEnumerator> func)>();

    protected void RegisterAction(float time, Func<IEnumerator> func)
    {
        this.actions.Add((time, func));
    }

    protected void RegisterAction(float minutes, float seconds, Func<IEnumerator> func) => RegisterAction(new TimeStamp(minutes, seconds), func);

    protected void RegisterAction(TimeStamp time, Func<IEnumerator> func)
    {
        this.actions.Add((time.GetTime(), func));
    }

    private void FixedUpdate()
    {
        List<(float time, Func<IEnumerator> func)> actionsToRemove = new List<(float time, Func<IEnumerator> func)>();

        foreach(var action in actions)
        {
            if(action.time < Time.timeSinceLevelLoad)
            {
                actionsToRemove.Add(action);
                StartCoroutine(action.func());
            }
        }

        foreach(var action in actionsToRemove)
        {
            actions.Remove(action);
        }

        ProtectedFixedUpdate();
    }

    protected abstract void ProtectedFixedUpdate();
}
