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
            GameObject camera = state.player.camera;
            Vector3 position = camera.transform.position;
            Vector3 forward = camera.transform.forward;

            if (Physics.Raycast(position, forward, out RaycastHit grappelHit, Player.grappleHookDistance, LayerMask.GetMask("Default")))
            {
                if (grappelHit.collider.tag == "Grappleable")
                {
                    //play sound here
                    Animator anim = state.player.GetComponent<Animator>();
                    state.player.behaviour = new GrappleHookBehaviour(grappelHit.point);
                    anim.SetBool("isGrappling", true);
                }
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

        if (!state.groundedLastFrame)
        {

            if (state.lastY < -25)
            {
                Debug.Log("Sound Playing");
                //play sound here
            }
        }
        if (state.buffer.superJumpPressed && !state.buffer.superJumpReleased)
        {
            state.player.setJetBurn(true);
        }
        else
        {
            state.player.setJetBurn(false);
        }

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

        if (state.rigidbody.velocity.y <= 0)
        {
            state.player.setJetBurn(false);
        }

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
            Animator anim = state.player.GetComponent<Animator>();
            Vector3 playerInput = Vector3.zero;
            if (Input.GetKey(KeyCode.A))
            {
                playerInput.x -= 1;
                if (anim.GetBool("inAir"))
                {
                    anim.SetBool("isDashing", true);
                    anim.SetInteger("direction", 3);
                    anim.Play("Left Dash");
                }
            }
            if (Input.GetKey(KeyCode.D))
            {
                playerInput.x += 1;
                if (anim.GetBool("inAir"))
                {
                    anim.SetBool("isDashing", true);
                    anim.SetInteger("direction", 1);
                    anim.Play("Right Dash");
                }
            }
            if (Input.GetKey(KeyCode.S))
            {
                playerInput.z -= 1;
                if (anim.GetBool("inAir"))
                {
                    anim.SetBool("isDashing", true);
                    anim.SetInteger("direction", 2);
                    anim.Play("Back Dash");
                }
            }
            if (Input.GetKey(KeyCode.W))
            {
                playerInput.z += 1;
                if (anim.GetBool("inAir"))
                {
                    anim.SetBool("isDashing", true);
                    anim.SetInteger("direction", 0);
                    anim.Play("Forward Dash");
                }
            }

            if (playerInput.magnitude > 1f)
            {
                playerInput = Vector3.Normalize(playerInput);
            }
            if (playerInput.magnitude == 0)
            {
                if (state.groundedLastFrame && PlayerPowerupState.hasSuperJump)
                    state.player.behaviour = new JumpCharge();
            }
            else
            {
                state.airDashCooldown = Time.time + 1f;
                state.airDash = false;
                Quaternion rotation;
                rotation = Quaternion.Euler(0f, state.player.mousePosition.x, 0f);
                Vector3 direction = rotation * playerInput;
                state.player.behaviour = new AirDashBehaviour(direction);
            }
        }
    }
}
