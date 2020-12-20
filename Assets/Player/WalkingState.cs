using UnityEngine;

public class WalkingBehaviour : PlayerBehaviour
{
    public override void Update(PlayerState state)
    {
        PhysicsUpdate(state);
    }

    private void PhysicsUpdate(PlayerState state)
    {
        if (state.buffer.grappelHook)
        {
            Camera camera = state.player.camera;
            Vector3 position = camera.transform.position;
            Vector3 forward = camera.transform.forward;

            if(Physics.Raycast(position, forward, out RaycastHit grappelHit, Mathf.Infinity, LayerMask.GetMask("Default")))
            {
                state.player.behaviour = new GrappelHookBehaviour(grappelHit.point);
            }
        }

        if (IsGrounded(state, out var hit))
        {
            GroundUpdate(state, hit);
            state.groundedLastFrame = true;
        }
        else
        {
            AirUpdate(state);
            state.groundedLastFrame = false;
        }
    }

    private bool IsGrounded(PlayerState state, out RaycastHit hit)
    {
        if (state.velocity.y > 0f)
        {
            hit = default;
            return false;
        }

        float distance = Player.groundDetectionDistance;
        if (!state.groundedLastFrame)
        {
            distance = Player.hoverDistance - 0.5f + 0.1f;
        }

        return Physics.BoxCast(state.player.transform.position + Vector3.one * 0.1f, Vector3.one * 0.5f, Vector3.down, out hit, Quaternion.identity, distance, LayerMask.GetMask("Default"));
    }

    private void GroundUpdate(PlayerState state, RaycastHit hit)
    {
        SnapToGround(state, hit);
        UpdateVelocty(state, Player.groundAcceleration, true, false);

        if (state.buffer.jump)
        {
            Vector3 v = state.velocity;
            v.y = Player.jumpSpeed;
            state.velocity = v;
        }

        state.hasGrappelJump = true;
    }

    private void SnapToGround(PlayerState state, RaycastHit hit)
    {
        Vector3 pos = state.player.transform.position;
        pos.y = hit.point.y + Player.hoverDistance;
        state.player.transform.position = pos;
    }

    private void UpdateVelocty(PlayerState state, float acceleration, bool flatten, bool applyGravity)
    {
        Vector3 velocity = state.velocity;
        Vector3 flatVelocity = velocity;
        flatVelocity.y = 0f;

        Vector3 input = Vector3.zero;
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

    private void AirUpdate(PlayerState state)
    {
        UpdateVelocty(state, Player.airAcceleration, false, true);
    }
}
