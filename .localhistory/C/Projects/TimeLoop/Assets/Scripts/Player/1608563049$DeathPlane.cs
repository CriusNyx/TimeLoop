using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPlane : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.name == "Player")
        {
            TimeLoopSceneManager sm = UnityEngine.Object.FindObjectOfType<TimeLoopSceneManager>();
            StartCoroutine(sm.TriggerDeath());
        }
    }
}
