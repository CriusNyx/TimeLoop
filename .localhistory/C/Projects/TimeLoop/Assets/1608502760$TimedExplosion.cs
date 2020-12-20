using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedExplosion : TimeBehaviour
{
    protected override void ProtectedFixedUpdate()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<ParticleSystem>().Stop();
        RegisterAction(0f, 1f, RoutineToDo);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator RoutineToDo()
    {
        GetComponent<ParticleSystem>().Play();
        // This behaviour will execute after a certain time once registered.
        yield return null;
    }
}
