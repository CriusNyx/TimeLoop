using UnityEngine;

public class PlayerState
{
    public Player player;

    public Rigidbody rigidbody;
    public Vector3 velocity = Vector3.zero;
    public bool groundedLastFrame = false;
    public bool hasGrappelJump = true;

    public bool airDash = true;
    public float airDashCooldown = -1f;
    public bool canGrapple = true;
    public float grappleHookCooldown = -1f;

    public bool canDoubleJump = false;

    public InputBuffer buffer = new InputBuffer(null);

    public PlayerState(Player player, Rigidbody rigidbody)
    {
        this.player = player;
        this.rigidbody = rigidbody;
    }
}
