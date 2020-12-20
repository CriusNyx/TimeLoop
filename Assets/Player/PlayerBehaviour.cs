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
}
