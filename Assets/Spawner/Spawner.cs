using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : TimeBehaviour
{
    public float minutes = 0f;
    public float seconds = 0f;
    public GameObject prefab;

    public void Start()
    {
        RegisterAction(minutes, seconds, Spawn);
    }

    private IEnumerator Spawn()
    {
        yield return null;

        GameObject instance = Instantiate(prefab, transform.position, transform.rotation);
        foreach(var onSpawn in instance.GetComponentsInChildren<IOnSpawn>())
        {
            onSpawn.OnSpawn(this);
        }
    }

    protected override void ProtectedFixedUpdate()
    {

    }
}
