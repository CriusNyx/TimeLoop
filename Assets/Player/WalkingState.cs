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
        state.airDash = true;

        CheckAirDash(state);
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
        CheckAirDash(state);
    }

    private static void CheckAirDash(PlayerState state)
    {
        if (state.buffer.airDashPressed && state.airDash && Time.time > state.airDashCooldown)
        {
            state.airDashCooldown = Time.time + 1f;
            state.airDash = false;

            Vector3 playerInput = Vector3.zero;
            if (Input.GetKey(KeyCode.A))
            {
                playerInput.x -= 1;
            }
            if (Input.GetKey(KeyCode.D))
            {
                playerInput.x += 1;
            }
            if (Input.GetKey(KeyCode.S))
            {
                playerInput.z -= 1;
            }
            if (Input.GetKey(KeyCode.W))
            {
                playerInput.z += 1;
            }

            if (playerInput.magnitude > 1f)
            {
                playerInput = Vector3.Normalize(playerInput);
            }
            if (playerInput.magnitude != 0)
            {
                Quaternion rotation = Quaternion.Euler(0f, state.player.mousePosition.x, 0f);
                Vector3 direction = rotation * playerInput;
                state.player.behaviour = new AirDashBehaviour(direction);
            }
        }
    }
}