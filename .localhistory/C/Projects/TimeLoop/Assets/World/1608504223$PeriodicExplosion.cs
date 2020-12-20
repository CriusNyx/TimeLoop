using UnityEngine;
using System.Collections;

public class PeriodicExplosion : MonoBehaviour
{
    SphereCollider killCollider;

    // Use this for initialization
    void Start()
    {
        GetComponent<ParticleSystem>().Stop();


        GetComponent<ParticleSystem>().Play();
        killCollider = gameObject.AddComponent<SphereCollider>();
        killCollider.radius = 3f;
        killCollider.isTrigger = true;

        gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");


        // This behaviour will execute after a certain time once registered.
        yield return new WaitForSeconds(GetComponent<ParticleSystem>().main.duration);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Explode()
    {

    }
}
