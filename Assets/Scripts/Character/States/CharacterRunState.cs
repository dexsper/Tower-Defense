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
            if (e.EnemyBase.IsDeath())
            {
                e.SetEnemyBase(null);
                return;
            }

            Vector3 targetPos = e.EnemyBase.transform.position;
            targetPos.z = e.transform.position.z;

            e.transform.position = Vector3.MoveTowards(e.transform.position, targetPos, e.Config.Speed * Time.deltaTime);
            e.transform.LookAt(e.EnemyBase.transform);

            Entity target = e.Search.FindTarget(e.transform.position, e.Config.ChaseDistance, e.EnemyLayer);

            if (target != null)
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

