public interface State
{
    StateId GetId();

    void Enter(BaseEnemy e);
    void Update();
    void Exit();
}

public enum StateId
{
    Run,
    Chase,
    Idle,
    Attack,
    Death
}