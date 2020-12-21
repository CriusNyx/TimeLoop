using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnThingToSpawnSpawns : TimeBehaviour, IOnSpawn
{
    public void OnSpawn(Spawner spawner)
    {
        //Debug.Log("I Have Spawned Dude");
    }

    protected override void ProtectedFixedUpdate()
    {
    }
}
