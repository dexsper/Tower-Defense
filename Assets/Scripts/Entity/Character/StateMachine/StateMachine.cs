using UnityEngine;

public class StateMachine
{
    public State[] states;

    public BaseEnemy enemy;

    public StateId currentState;

    public StateMachine(BaseEnemy enemy)
    {
        this.enemy = enemy;

        int numStates = System.Enum.GetNames(typeof(StateId)).Length;
        states = new State[numStates];
    }

    public void RegisterState(State state)
    {
        int index = (int)state.GetId();
        states[index] = state;
    }

    public State GetState(StateId stateId)
    {
        int index = (int)stateId;
        return states[index];
    }

    public void Update()
    {
        GetState(currentState)?.Update(enemy);
    }

    public void ChangeState(StateId newState)
    {
        GetState(currentState)?.Exit(enemy);

        currentState = newState;

        GetState(currentState)?.Enter(enemy);

    }
}