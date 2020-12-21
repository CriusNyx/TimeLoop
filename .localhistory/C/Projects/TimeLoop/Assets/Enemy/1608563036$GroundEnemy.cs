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
    public bool fixedRotation = false;
    public float rotationIfFixed = 90f;
    Transform t;
    void Start()
    {
        t = transform;
    }

    // Update is called once per frame
    void Update()
    {
        counter++;
        if (counter >= (60f / shotsPerSeconds))
        {
            FireBullets();
            counter -= (60f / shotsPerSeconds);
        }
        if(fixedRotation == true)
        {
            t.eulerAngles = new Vector3(t.eulerAngles.x, rotationIfFixed, t.eulerAngles.z);
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
            StartCoroutine(sm.TriggerDeath());
        }
        
    }
}