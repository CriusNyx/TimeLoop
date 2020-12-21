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

    private void CreatePickupHintText(string text)
    {
        GameObject textObject = new GameObject("text");
        textObject.AddComponent<TextMesh>();

        TextMesh textMeshComponent = textObject.GetComponent(typeof(TextMesh)) as TextMesh;

        MeshRenderer meshRendererComponent = textObject.GetComponent(typeof(MeshRenderer)) as MeshRenderer;

        textMeshComponent.text = text;
        textMeshComponent.fontSize = 50;
        textMeshComponent.anchor = TextAnchor.MiddleCenter;
        textMeshComponent.alignment = TextAlignment.Center;


        textObject.transform.position = transform.position;
        Quaternion q = Camera.main.transform.rotation;
        q.eulerAngles = new Vector3(0, q.eulerAngles.y, q.eulerAngles.z);
        textObject.transform.rotation = q;

    textObject.transform.localScale = Vector3.one * 0.1f;
    }

    protected override void OnPickup(Collider other)
    {
        if(other.tag == "Player")
        {
            switch (powerupType)
            {
                case PowerupType.Grapple:
                    PlayerPowerupState.hasGrappleUnlocked = true;
                    CreatePickupHintText("The grapple can be used on floating rocks!");
                    break;
                case PowerupType.Jet:
                    PlayerPowerupState.hasSuperJump = true;
                    CreatePickupHintText("Hold shift while standing still to charge your jet!");
                    break;
                case PowerupType.Card:
                    playerInventory.AddItem(new KeyItem(0, Color.red));
                    break;
            }
        
            Destroy(gameObject);
        }
    }
}
