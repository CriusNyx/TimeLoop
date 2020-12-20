using UnityEngine;

public class WalkingBehaviour : PlayerBehaviour
{
    public override void Update(PlayerState state)
    {
        PhysicsUpdate(state);
    }

    private void PhysicsUpdate(PlayerState state)
    {
        if (state.buffer.grappelHook && state.canGrapple && PlayerPowerupState.hasGrappleUnlocked)
        {
            Camera camera = state.player.camera;
            Vector3 position = camera.transform.position;
            Vector3 forward = camera.transform.forward;

            if (Physics.Raycast(position, forward, out RaycastHit grappelHit, Player.grappleHookDistance, LayerMask.GetMask("Default")))
            {
                state.player.behaviour = new GrappleHookBehaviour(grappelHit.point);
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
        UpdateVelocty(state, Player.groundAcceleration, true, false, true);

        if (state.buffer.jump)
        {
            float y = Player.jumpSpeed;

            state.velocity.y = y;
        }

        state.hasGrappelJump = true;
        state.airDash = true;
        state.canDoubleJump = true;
        state.canGrapple = true;

        CheckAirDash(state);
    }

    private void AirUpdate(PlayerState state)
    {
        UpdateVelocty(state, Player.airAcceleration, false, true, true);
        CheckAirDash(state);

        if (state.buffer.jump && state.canDoubleJump)
        {
            state.canDoubleJump = false;
            state.velocity.y = Player.jumpSpeed;
        }
    }

    private static void CheckAirDash(PlayerState state)
    {
        if (state.buffer.airDashPressed && state.airDash && Time.time > state.airDashCooldown)
        {
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
            if (playerInput.magnitude == 0)
            {
                if (state.groundedLastFrame)
                    state.player.behaviour = new JumpCharge();
            }
            else
            {
                state.airDashCooldown = Time.time + 1f;
                state.airDash = false;
                Quaternion rotation = Quaternion.Euler(0f, state.player.mousePosition.x, 0f);
                Vector3 direction = rotation * playerInput;
                state.player.behaviour = new AirDashBehaviour(direction);
            }
        }
    }
}
