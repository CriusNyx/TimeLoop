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
        /* EnemyController = Enemy.GetComponent<UnityEngine.AI.NavMeshAgent>();
         rb.AddForce(new Vector3(EnemyController.velocity.x, 0, EnemyController.velocity.z) * speed); */
    }

    // Update is called once per frame
    void Update()
    { 
    }
}
