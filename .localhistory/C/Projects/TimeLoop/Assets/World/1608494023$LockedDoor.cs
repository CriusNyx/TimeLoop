using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedDoor : MonoBehaviour
{
    public int code;
    public bool locked = true;
    float openTimer = 0;
    public float openDuration = 0.1f;
    Vector3 initialPosition;
    Vector3 openPosition;

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
        openPosition = transform.position + Vector3.up * transform.localScale.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (!locked)
        {
            openTimer += Time.deltaTime / openDuration;
            transform.position = Vector3.Lerp(initialPosition, openPosition, openTimer);
        }
    }
    public void Open()
    {
        if(locked)
        {
            locked = false;
            Debug.Log("I HAVE BEEN UNLOCKED AH");
            // move up lol
        }
    }
}
