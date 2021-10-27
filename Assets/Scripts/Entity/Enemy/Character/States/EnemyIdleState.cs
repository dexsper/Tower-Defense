
class EnemyIdleState : State
{
    BaseEnemy enemy;

    public void Enter(BaseEnemy e)
    {
        enemy = e;
    }

    public void Exit()
    {
    }

    public StateId GetId()
    {
        return StateId.Idle;
    }

    public void Update()
    {
        if(enemy.EnemyBase != null)
        {
            enemy.stateMachine.ChangeState(StateId.Run);
        }      
    }
}

