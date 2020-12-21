using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{


    BoxCollider boxCollider;
    // Start is called before the first frame update
    void Start()
    {
        boxCollider = gameObject.AddComponent<BoxCollider>();
        boxCollider.size = Vector3.one * 3f;
        boxCollider.isTrigger = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.GetComponent<Player>())
        {
            Debug.Log("wow");
        }
    }
}
