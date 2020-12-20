using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayedExplosion : TimeBehaviour
{
    public float startMinute;
    public float startSecond;

    SphereCollider collider;

    protected override void ProtectedFixedUpdate()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<ParticleSystem>().Stop();
        RegisterAction(startMinute, startSecond, RoutineToDo);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator RoutineToDo()
    {
        GetComponent<ParticleSystem>().Play();
        collider = gameObject.AddComponent<SphereCollider>();
        collider.radius = 3f;
        collider.isTrigger = true;

        gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");


        // This behaviour will execute after a certain time once registered.
        yield return null;
    }
}
