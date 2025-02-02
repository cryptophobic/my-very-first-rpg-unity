using UnityEngine;

public class PlayerPrimaryAttackState : PlayerState
{
    private int comboCounter;

    private float lastTimeAttacked;
    private float comboWindow = 0.5f;
    
    public PlayerPrimaryAttackState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        xInput = 0;

        if (comboCounter > 2 || Time.time > lastTimeAttacked + comboWindow)
        {
            comboCounter = 0;
        }
        
        player.anim.SetInteger("ComboCounter", comboCounter);

        float attackDir = player.facingDir;
        // if (xInput != 0)
        // {
        //     attackDir = xInput;
        // }
        
        player.SetVelocity(player.attackMovement[comboCounter].x * attackDir, player.attackMovement[comboCounter].y);

        stateTimer = .2f;
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer < 0f)
        {
            player.SetZeroVelocity();
        }

        if (triggerCalled)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }

    public override void Exit()
    {
        base.Exit();
        player.StartCoroutine(nameof(player.BusyFor), .2f);
        
        comboCounter++;
        lastTimeAttacked = Time.time;
    }
}
