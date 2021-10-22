using UnityEngine;

public class CharacterAttackState : State
{
    public void Enter(BaseEnemy e)
    {
        e.Anim.SetBool("Attack", true);
    }

    public void Exit(BaseEnemy e)
    {
        e.Anim.SetBool("Attack", false);
    }

    public StateId GetId()
    {
        return StateId.Attack;
    }

    public void Update(BaseEnemy e)
    {
        if(e.Target.IsDeath())
        {
            e.SetTarget(null);
        }

        if(e.Target == null)
        {
            e.stateMachine.ChangeState(StateId.Run);
        }

        else if (Vector3.Distance(e.transform.position, e.Target.GetObject().transform.position) > e.Config.AttackDistance)
        {
            e.stateMachine.ChangeState(StateId.Chase);
        }

    }
}
