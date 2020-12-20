using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode : MonoBehaviour
{
    public PathNode next;

    private void OnDrawGizmos()
    {
        if (next != null)
        {
            Gizmos.DrawLine(transform.position, next.transform.position);
        }
    }
}