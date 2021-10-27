public interface State
{
    StateId GetId();

    void Enter(BaseEnemy e);
    void Update(BaseEnemy e);
    void Exit(BaseEnemy e);
}

public enum StateId
{
    Run,
    Chase,
    Idle,
    Attack,
    Death
}