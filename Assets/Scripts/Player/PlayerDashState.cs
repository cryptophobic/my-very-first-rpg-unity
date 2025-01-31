using UnityEngine;

public class PlayerDashState : PlayerState
{
    private GameObject playerObject;
    
    public PlayerDashState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
        playerObject = GameObject.Find("Player");
    }

    public override void Enter()
    {
        base.Enter();
        playerObject.layer = LayerMask.NameToLayer("PlayerDash");
        stateTimer = player.dashDuration;
    }

    public override void Update()
    {
        base.Update();

        if (!player.IsGroundDetected() && player.IsWallDetected())
        {
            stateMachine.ChangeState(player.wallSlideState);
        }
        
        player.SetVelocity(player.dashSpeed * player.dashDir, 0);
        if (stateTimer < 0f)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }

    public override void Exit()
    {
        base.Exit();
        player.SetVelocity(0, rb.linearVelocity.y);
        playerObject.layer = LayerMask.NameToLayer("Player");
    }
}
