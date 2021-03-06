using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedDoor : MonoBehaviour
{
    public int code;
    public bool locked = true;
    float openTimer = 0;
    public float openDuration = 0.5f; // in seconds
    Vector3 initialPosition;
    Vector3 openPosition;

    protected BoxCollider boxCollider;

    private void Awake()
    {
        boxCollider = gameObject.AddComponent<BoxCollider>();
        boxCollider.size = Vector3.one * 5;
        boxCollider.isTrigger = true;

        gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
    }

    private void OnTriggerEnter(Collider other)
    {
        Inventory inventory = other.GetComponent<Inventory>();
        if (inventory)
        {
            Item[] items = inventory.GetItems();
            for(var i = 0; i < items.Length; i++)
            {
                if (items[i].GetType() == typeof(KeyItem))
                {
                    Open();
                    items[i] = null;
                }
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
        openPosition = transform.position + Vector3.up * transform.localScale.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (!locked)
        {
            openTimer += Time.deltaTime / openDuration;
            transform.position = Vector3.Lerp(initialPosition, openPosition, openTimer);
        }
    }
    public void Open()
    {
        locked = false;
    }
}
