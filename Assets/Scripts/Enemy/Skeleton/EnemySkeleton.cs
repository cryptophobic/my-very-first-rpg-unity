using UnityEngine;

public class EnemySkeleton : Enemy
{
    #region States
    public SkeletonIdleState idleState { get; private set; }
    public SkeletonMoveState moveState { get; private set; }
    public SkeletonBattleState battleState { get; private set; } 
    public SkeletonAttackState attackState { get; private set; }
    public SkeletonStunnedState StunnedState { get; private set; }
    #endregion
    
    protected override void Awake()
    {
        base.Awake();

        idleState = new SkeletonIdleState(this, stateMachine, "Idle", this);
        moveState = new SkeletonMoveState(this, stateMachine, "Move", this);
        battleState = new SkeletonBattleState(this, stateMachine, "Move", this);
        attackState = new SkeletonAttackState(this, stateMachine, "Attack", this);
        StunnedState = new SkeletonStunnedState(this, stateMachine, "Stunned", this);
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
    }

    protected override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.Q))
        {
            stateMachine.ChangeState(StunnedState);
        }
    }
}
