using UnityEngine;

public class SkeletonStunnedState : EnemyState
{
    private EnemySkeleton enemy;
    
    public SkeletonStunnedState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, EnemySkeleton enemy) : base(enemyBase, stateMachine, animBoolName)
    {
        this.enemy = enemy;
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer < 0)
        {
            stateMachine.ChangeState(enemy.idleState);
        }
    }

    public override void Enter()
    {
        base.Enter();
        
        enemy.fx.InvokeRepeating("RedColorBlink", 0f, 0.1f);
        
        stateTimer = enemy.stunDuration;
        enemy.SetVelocity(-enemy.facingDir * enemy.stunDirection.x, enemy.stunDirection.y, true);
    }

    public override void Exit()
    {
        base.Exit();
        enemy.fx.Invoke("CancelRedBlink", 0);
    }
}
