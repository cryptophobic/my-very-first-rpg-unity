using UnityEngine;

public class SkeletonBattleState : EnemyState
{
    private Transform player;
    private EnemySkeleton enemy;
    private int moveDir;
    public SkeletonBattleState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, EnemySkeleton enemy) : base(enemyBase, stateMachine, animBoolName)
    {
        this.enemy = enemy;
    }
    
    public override void Update()
    {
        base.Update();

        if (enemy.IsPlayerDetected())
        {
            if (enemy.IsPlayerDetected().distance < enemy.attackDistance && CanAttack())
            {
                stateMachine.ChangeState(enemy.attackState);
            }
        }
        
        moveDir = player.position.x > enemy.transform.position.x ? 1 : -1;
        
        enemy.SetVelocity(enemy.moveSpeed * moveDir, rb.linearVelocityY);
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

    private bool CanAttack()
    {
        return Time.time >= enemy.lastTimeAttacked + enemy.attackCooldown;
    }
}
