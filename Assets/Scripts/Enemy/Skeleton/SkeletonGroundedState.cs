using UnityEngine;

public class SkeletonGroundedState : EnemyState
{
    protected EnemySkeleton enemy;

    protected Transform player;
    
    public SkeletonGroundedState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, EnemySkeleton enemy) : base(enemyBase, stateMachine, animBoolName)
    {
        this.enemy = enemy;
    }

    public override void Update()
    {
        base.Update();

        if (enemy.IsPlayerDetected() || Vector2.Distance(enemy.transform.position, player.position) < 2f)
        {
            stateMachine.ChangeState(enemy.battleState);
        }
    }

    public override void Enter()
    {
        base.Enter();
        
        player = GameObject.Find("Player").transform;
    }

    public override void Exit()
    {
        base.Exit();
    }
}
