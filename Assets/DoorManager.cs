using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    public GameObject[] doors;

    private int doorPtr = -1;

    public void UnlockNextDoor()
    {
        doorPtr++;
        if(doorPtr < doors.Length)
        {
            doors[doorPtr].GetComponent<LockedDoor>().locked = false;
        }
    }
}
