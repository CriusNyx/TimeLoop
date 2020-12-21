using System;
using UnityEngine;

public class Powerup : Pickup
{
    public enum PowerupType
    {
        Grapple,
        Jet,
        Card
    }

    public PowerupType powerupType;
    public Color color = Color.black;


    private Vector3 start;
    private float rotAng = 0f;
    private Inventory playerInventory;
    private void Start()
    {
        start = transform.position;
        bool shouldDelete = powerupType switch
        {
            PowerupType.Grapple => PlayerPowerupState.hasGrappleUnlocked,
            PowerupType.Jet => PlayerPowerupState.hasSuperJump,
            _ => false
        };

        if (shouldDelete)
        {
            Destroy(gameObject);
        }

        playerInventory = GameObject.Find("Player").GetComponent<Inventory>();
    }

    private void Update()
    {
        transform.Rotate(Vector3.up, 90f * Time.deltaTime);
        rotAng += 1.5f * Time.deltaTime;
        rotAng %= 2 * Mathf.PI;
        transform.position = start + new Vector3(0, Mathf.Sin(rotAng)*.25f, 0);
    }

    protected override void OnPickup(Collider other)
    {
        if(other.tag == "Player")
        {
            switch (powerupType)
            {
                case PowerupType.Grapple:
                    PlayerPowerupState.hasGrappleUnlocked = true;
                    break;
                case PowerupType.Jet:
                    PlayerPowerupState.hasSuperJump = true;
                    break;
                case PowerupType.Card:
                    playerInventory.AddItem(new KeyItem());
                    break;
            }
            Destroy(gameObject);
        }
    }
}
