using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    public float speed = 10f;
    private Rigidbody rb;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = (transform.forward  * speed);
        Destroy(gameObject, 4);
    }

    // Update is called once per frame
    void Update()
    { 
    }

    void OnTriggerEnter(Collider c)
    {
        if(c.gameObject.name == "Player")
        {
            TimeLoopSceneManager sm = UnityEngine.Object.FindObjectOfType<TimeLoopSceneManager>();
            StartCoroutine(sm.TriggerDeath());
        }
    }
}
