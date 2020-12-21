using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundEnemy : MonoBehaviour
{
    public GameObject Projectile;
    public float bulletSpawnHeight = 1.5f;
    public float spawnDistance = 1.25f;
    public int shotsPerSeconds = 2;
    private float counter = 0;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        counter += Time.deltaTime;
        Debug.Log(counter);
        if (counter >= (1 / shotsPerSeconds))
        {
            FireBullets();
            counter -= (1 / shotsPerSeconds);
        }
    }
    void FireBullets()
    {
        GameObject bullet = Instantiate(Projectile, (bulletSpawnHeight * Vector3.up) + transform.position + transform.forward * spawnDistance, transform.rotation) as GameObject;
    }
    void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.name == "Player")
        {
            TimeLoopSceneManager sm = UnityEngine.Object.FindObjectOfType<TimeLoopSceneManager>();
            sm.TriggerDeath();
        }
        
    }
}