using UnityEngine;
using System.Collections;

public class PeriodicExplosion : TimeBehaviour
{
    public float startMinute;
    public float startSecond;

    SphereCollider killCollider;

    // Use this for initialization
    void Start()
    {
        GetComponent<ParticleSystem>().Stop();
        RegisterAction(startMinute, startSecond, RoutineToDo);

        killCollider = gameObject.AddComponent<SphereCollider>();
        killCollider.radius = 3f;
        killCollider.isTrigger = true;

        gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");


        killCollider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

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
        yield return new WaitForSeconds(GetComponent<ParticleSystem>().main.duration);
        Destroy(gameObject);
    }

    protected override void ProtectedFixedUpdate()
    {
        throw new System.NotImplementedException();
    }
}
