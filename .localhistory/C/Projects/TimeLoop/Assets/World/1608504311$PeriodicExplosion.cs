using UnityEngine;
using System.Collections;

public class PeriodicExplosion : TimeBehaviour
{
    public float startMinute;
    public float startSecond;
    public float period;

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
        killCollider.enabled = true;


        while (true)
        {
            yield return new WaitForSeconds(period);
        }
    }

    protected override void ProtectedFixedUpdate()
    {
        throw new System.NotImplementedException();
    }
}
