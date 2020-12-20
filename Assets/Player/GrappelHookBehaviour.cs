using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappelHookBehaviour : PlayerBehaviour
{
    public Vector3 target;

    public GrappelHookBehaviour(Vector3 target)
    {
        this.target = target;
    }

    public override void Update(PlayerState state)
    {
        LineRenderer grappleLineRenderer = GameObject.Find("GrappleLineRenderer").GetComponent<LineRenderer>();
        grappleLineRenderer.SetPositions(new Vector3[] { state.player.transform.position, target });
        grappleLineRenderer.widthMultiplier = 0.1f;
        grappleLineRenderer.enabled = true;

        if (state.buffer.jump)
        {
            grappleLineRenderer.enabled = false;

            if (state.hasGrappelJump)
            {
                state.velocity.y = Player.jumpSpeed;
                state.hasGrappelJump = false;
            }
            state.player.behaviour = new WalkingBehaviour();
        }
        else if (state.buffer.grappelHook)
        {
            grappleLineRenderer.enabled = false;
            state.player.behaviour = new WalkingBehaviour();
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
    }
}
