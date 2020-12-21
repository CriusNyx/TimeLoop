using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public abstract class PlayerBehaviour
{
    public abstract void Update(PlayerState state);

    protected bool IsGrounded(PlayerState state, out RaycastHit hit)
    {
        if (state.velocity.y > 0f)
        {
            hit = default;
            return false;
        }

        float distance = Player.groundDetectionDistance + 0.6f;

        if (!state.groundedLastFrame)
        {
            distance = Player.hoverDistance - 0.5f + 0.6f;
        }

        return Physics.BoxCast(state.player.transform.position + Vector3.one * 0.6f, Vector3.one * 0.5f, Vector3.down, out hit, Quaternion.identity, distance, LayerMask.GetMask("Default"));
    }

    protected void SnapToGround(PlayerState state, RaycastHit hit)
    {
        Vector3 pos = state.player.transform.position;
        pos.y = hit.point.y + Player.hoverDistance;
        state.player.transform.position = pos;
    }

    protected void UpdateVelocty(PlayerState state, float acceleration, bool flatten, bool applyGravity, bool doInput)
    {
        Vector3 velocity = state.velocity;
        Vector3 flatVelocity = velocity;
        flatVelocity.y = 0f;

        Vector3 input = Vector3.zero;
        if (doInput)
        {
            if (Input.GetKey(KeyCode.A))
            {
                input.x -= 1f;
            }
            if (Input.GetKey(KeyCode.D))
            {
                input.x += 1f;
            }
            if (Input.GetKey(KeyCode.S))
            {
                input.z -= 1f;
            }
            if (Input.GetKey(KeyCode.W))
            {
                input.z += 1f;
            }
        }

        if (input.magnitude > 1f)
        {
            input = input.normalized;
        }

        Quaternion inputOrientation = Quaternion.Euler(0f, state.player.mousePosition.x, 0f);

        Vector3 targetVelocty = inputOrientation * input * Player.maxVelocity;

        flatVelocity = Vector3.MoveTowards(flatVelocity, targetVelocty, acceleration * Time.fixedDeltaTime);

        if (!flatten)
        {
            flatVelocity.y = velocity.y;
        }
        if (applyGravity)
        {
            flatVelocity.y += -Player.gravityAcceleration * Time.fixedDeltaTime;
        }

        state.velocity = flatVelocity;
    }
}
