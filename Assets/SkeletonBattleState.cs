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
            stateTimer = enemy.battleTime;
            
            if (enemy.IsPlayerDetected().distance < enemy.attackDistance && CanAttack())
            {
                stateMachine.ChangeState(enemy.attackState);
            }
        }
        else
        {
            if (stateTimer < 0 || Vector2.Distance(player.transform.position, enemy.transform.position) > 7)
            {
                stateMachine.ChangeState(enemy.idleState);
            } 
        }
        
        moveDir = player.position.x > enemy.transform.position.x ? 1 : -1;
        
        enemy.SetVelocity(enemy.moveSpeed * moveDir * 2, rb.linearVelocityY);
    }

    public override void Enter()
    {
        base.Enter();
        enemy.transform.localScale = new Vector3(1.75f, 1.75f, 1.75f);
        player = GameObject.Find("Player").transform;
    }

    public override void Exit()
    {
        enemy.transform.localScale = new Vector3(1.4f, 1.4f, 1.4f);
        base.Exit();
    }

    private bool CanAttack()
    {
        return Time.time >= enemy.lastTimeAttacked + enemy.attackCooldown;
    }
}
