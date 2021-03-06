using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDestroyWhenDone : MonoBehaviour
{
    ParticleSystem system;

    // Start is called before the first frame update
    void Start()
    {
        system = gameObject.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if(system.particleCount == 0)
        {
            Destroy(gameObject);
        }
    }
}
