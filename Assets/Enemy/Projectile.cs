using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    public float speed = 10f;
    public float lifespan = 5f;
    public GameObject Enemy;
    public UnityEngine.AI.NavMeshAgent EnemyController;
    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = (transform.forward  * speed);
    }

    // Update is called once per frame
    void Update()
    { 
    }
}
