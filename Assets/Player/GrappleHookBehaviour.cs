using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleHookBehaviour : PlayerBehaviour
{
    public Vector3 target;
    private float timeToStop;

    public GrappleHookBehaviour(Vector3 target)
    {
        this.target = target;
        timeToStop = Time.time + Player.grappleHookTimeout;
    }

    public override void Update(PlayerState state)
    {
        LineRenderer grappleLineRenderer = GameObject.Find("GrappleLineRenderer").GetComponent<LineRenderer>();
        grappleLineRenderer.SetPositions(new Vector3[] { state.player.transform.position, target });
        grappleLineRenderer.widthMultiplier = 0.1f;
        grappleLineRenderer.enabled = true;

        if (Time.time > timeToStop)
        {
            Break(state, grappleLineRenderer, false);
        }
        if (state.buffer.jump && state.hasGrappelJump)
        {
            Break(state, grappleLineRenderer, state.hasGrappelJump);
        }
        else if (state.buffer.grappelHook)
        {
            Break(state, grappleLineRenderer, false);
        }
        else
        {
            Vector3 delta = target - state.player.transform.position;
            delta = Vector3.Normalize(delta);
            Vector3 accel = delta * Player.gravityAcceleration * 3f;
            accel += Vector3.down * Player.gravityAcceleration;
            state.velocity += accel * Time.fixedDeltaTime;

            state.groundedLastFrame = false;
        }

        state.grappleHookCooldown = Time.time + Player.grappleHookCooldown;
    }

    public void Break(PlayerState state, LineRenderer grappleLineRenderer, bool jump)
    {
        if (jump)
        {
            state.hasGrappelJump = false;
            state.velocity.y = Player.jumpSpeed;
        }

        grappleLineRenderer.enabled = false;
        state.player.behaviour = new WalkingBehaviour();
    }
}
