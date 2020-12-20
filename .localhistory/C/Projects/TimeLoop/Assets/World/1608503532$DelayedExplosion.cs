using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayedExplosion : TimeBehaviour
{
    public float startMinute;
    public float startSecond;

    SphereCollider killCollider;

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
        if (!GetComponent<ParticleSystem>().IsAlive())
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Player>();
        if (player)
        {
            //player.Kill();
            Debug.Log("dead");
        }
    }

    private IEnumerator RoutineToDo()
    {
        Debug.Log("explode");
        GetComponent<ParticleSystem>().Play();
        killCollider = gameObject.AddComponent<SphereCollider>();
        killCollider.radius = 3f;
        killCollider.isTrigger = true;

        gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");


        // This behaviour will execute after a certain time once registered.
        yield return null;
    }
}
