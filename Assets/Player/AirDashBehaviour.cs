using UnityEngine;

public class AirDashBehaviour : PlayerBehaviour
{
    public readonly Vector3 direction;
    private float timeToStop;
    public GameObject particleEmmitter;

    public AirDashBehaviour(Vector3 direction)
    {
        particleEmmitter = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/SpeedLines"));

        this.direction = direction; ;
        timeToStop = Time.time + 0.1f;
    }

    public override void Update(PlayerState state)
    {
        particleEmmitter.transform.parent = state.player.transform;
        particleEmmitter.transform.rotation = Quaternion.LookRotation(-direction);
        particleEmmitter.transform.position = state.player.transform.position + direction * 20f;

        state.velocity = direction * Player.airDashSpeed;
        if(Time.time > timeToStop)
        {
            particleEmmitter.GetComponent<ParticleSystem>().Stop();
            state.player.behaviour = new WalkingBehaviour();
            state.velocity = direction * Player.maxVelocity;
            particleEmmitter.transform.parent = null;
            particleEmmitter.AddComponent<ParticleDestroyWhenDone>();
        }

        if (IsGrounded(state, out var hit))
        {
            SnapToGround(state, hit);
            state.groundedLastFrame = true;
        }
        else
        {
            state.groundedLastFrame = false;
        }
    }
}