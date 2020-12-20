using UnityEngine;

public class Powerup : Pickup
{
    public enum PowerupType
    {
        Grapple
    }

    public PowerupType powerupType;
    public Color color = Color.black;

    private void Start()
    {
        bool shouldDelete = powerupType switch
        {
            PowerupType.Grapple => PlayerPowerupState.hasGrappleUnlocked,
            _ => false
        };

        if (shouldDelete)
        {
            Destroy(gameObject);
        }
    }

    protected override void OnPickup(Collider other)
    {
        switch (powerupType)
        {
            case PowerupType.Grapple:
                PlayerPowerupState.hasGrappleUnlocked = true;
                break;
        }
        Destroy(gameObject);
    }
}
