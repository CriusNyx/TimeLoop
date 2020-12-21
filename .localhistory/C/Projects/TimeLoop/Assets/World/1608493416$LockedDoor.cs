using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedDoor : MonoBehaviour
{
    public int code;
    public bool locked = true;
    float openTimer = 0;
    public float openDuration = 5f;
    Vector3 initialPosition;
    public Vector3 openPosition;

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        openTimer += Time.deltaTime / openDuration;
        transform.position = Vector3.Lerp(initialPosition, target, openTimer);
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
