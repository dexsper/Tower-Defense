using System.Linq;
using UnityEngine;

class CharacterRunState : State
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
        return StateId.Run;
    }

    

    public void Update(BaseEnemy e)
    {
        if(e.Target != null)
        {
            e.stateMachine.ChangeState(StateId.Chase);
        }

        if(e.EnemyBase != null)
        {
            e.transform.position = Vector3.MoveTowards(e.transform.position, e.EnemyBase.transform.position, e.Config.Speed * Time.deltaTime);
            e.transform.LookAt(e.EnemyBase.transform);

            Collider[] colliders = Physics.OverlapSphere(e.transform.position, e.Config.ChaseDistance, e.EnemyLayer);

            IDamageble target = colliders.OrderBy(c => Vector3.Distance(e.transform.position, c.transform.position))
                .FirstOrDefault()?.GetComponent<IDamageble>();

            if(target != null)
            {
                e.SetTarget(target);

                e.stateMachine.ChangeState(StateId.Chase);
            }
        }
        else
        {
            e.stateMachine.ChangeState(StateId.Idle);
        }

    }
}

