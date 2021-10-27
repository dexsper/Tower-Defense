
class CharacterIdleState : State
{
    public void Enter(BaseEnemy e)
    {
    }

    public void Exit(BaseEnemy e)
    {
    }

    public StateId GetId()
    {
        return StateId.Idle;
    }

    public void Update(BaseEnemy e)
    {
        if(e.EnemyBase != null)
        {
            e.stateMachine.ChangeState(StateId.Run);
        }      
    }
}

