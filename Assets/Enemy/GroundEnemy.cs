using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundEnemy : MonoBehaviour
{
    public GameObject Projectile;
    public float bulletSpawnHeight = 1.5f;
    public float spawnDistance = 1.25f;
    private int counter = 0;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        counter++;
        if (counter == 960)
        {
            FireBullets();
            counter = 0;
        }
    }
    void FireBullets()
    {
        GameObject bullet = Instantiate(Projectile, (bulletSpawnHeight * Vector3.up) + transform.position + transform.forward * spawnDistance, transform.rotation) as GameObject;
    }
}