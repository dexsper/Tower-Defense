using System.Linq;
using UnityEngine;

class CharacterChaseState: State
{
    public void Enter(BaseEnemy e)
    {
        e.Anim.SetBool("Run", true);
    }

    public void Exit(BaseEnemy e)
    {
        e.Anim.SetBool("Run", false);
    }

    public StateId GetId()
    {
        return StateId.Chase;
    }

    public void Update(BaseEnemy e)
    {
        if(e.Target == null)
        {
            e.stateMachine.ChangeState(StateId.Run);
        }
        else
        {
            float distance = Vector3.Distance(e.transform.position, e.Target.transform.position);

            if (distance < e.Config.AttackDistance)
            {
                e.stateMachine.ChangeState(StateId.Attack);
            }
            else 
            {
                if(distance > e.Config.ChaseDistance)
                {
                    e.SetTarget(null);
                    e.stateMachine.ChangeState(StateId.Run);
                    return;
                }

                if (e.Target)
                {
                    e.transform.position = Vector3.MoveTowards(e.transform.position, e.Target.transform.position, e.Config.Speed * Time.deltaTime);
                    e.transform.LookAt(e.Target.transform);
                }
            }
        }
    }
}

