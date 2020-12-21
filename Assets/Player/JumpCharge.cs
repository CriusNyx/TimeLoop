using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class JumpCharge : PlayerBehaviour
{
    public float holdTime = 0f;

    public override void Update(PlayerState state)
    {
        holdTime += Time.fixedDeltaTime;

        if (state.buffer.superJumpReleased)
        {
            float t = Mathf.Clamp01(holdTime);
            float v = Mathf.Lerp(Player.jumpSpeed, Player.highJumpSpeed, t);

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

            Quaternion rotation = Quaternion.Euler(0f, state.player.mousePosition.x, 0f);
            Vector3 direction = rotation * playerInput;

            if (playerInput.magnitude == 0f)
            {
                state.velocity = Vector3.up * v;
            }
            else
            {
                Vector3 side = direction * Mathf.Cos(Mathf.PI / 4f) * v;
                Vector3 up = Vector3.up * Mathf.Sin(Mathf.PI / 4f) * v;
                state.velocity = side * 1.5f + up;
            }
            //Debug.Log(state.velocity);
            state.player.anim.SetBool("jumpCharged", true);
            MusicSelector m = UnityEngine.Object.FindObjectOfType<MusicSelector>();
            m.startPlayingWoosh();
            state.player.behaviour = new WalkingBehaviour();
        }
        else
        {
            UpdateVelocty(state, Player.groundAcceleration, true, false, false);
        }
    }
}