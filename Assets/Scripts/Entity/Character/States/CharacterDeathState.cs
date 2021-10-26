
class CharacterDeathState : State
{
    public void Enter(BaseEnemy e)
    {
        e.Anim.SetTrigger("Death");
    }

    public void Exit(BaseEnemy e)
    {
    }

    public StateId GetId()
    {
        return StateId.Death;
    }

    public void Update(BaseEnemy e)
    {
        
    }
}

