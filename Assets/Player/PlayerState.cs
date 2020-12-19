using UnityEngine;

public class PlayerState
{
    public Player player;

    public Rigidbody rigidbody;
    public Vector3 velocity = Vector3.zero;
    public bool groundedLastFrame = false;

    public InputBuffer buffer = new InputBuffer();


    public PlayerState(Player player, Rigidbody rigidbody)
    {
        this.player = player;
        this.rigidbody = rigidbody;
    }
}
