using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD : TimeBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        RegisterAction(0f, 1f, RoutineToDo);
    }

    // Update is called once per frame
    void Update()
    {
        
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
