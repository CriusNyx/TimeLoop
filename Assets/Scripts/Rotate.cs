using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float speed = 5f;

    public void Update()
    {
        transform.rotation = Quaternion.Euler(0f, Time.deltaTime * speed, 0f) * transform.rotation;
    }
}
