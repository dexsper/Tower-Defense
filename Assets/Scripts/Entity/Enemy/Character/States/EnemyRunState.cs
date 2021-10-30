using System.Linq;
using UnityEngine;

class EnemyRunState : State
{
    BaseEnemy enemy;

    public void Enter(BaseEnemy e)
    {
        enemy = e;
        enemy.Anim.SetBool("Run", true);
    }

    public void Exit()
    {
        if(enemy)
            enemy.Anim.SetBool("Run", false);
    }

    public StateId GetId()
    {
        return StateId.Run;
    }

    

    public void Update()
    {
        if(enemy.Target != null)
        {
            enemy.stateMachine.ChangeState(StateId.Chase);
        }


        if(enemy.EnemyBase != null)
        {
            if (enemy.EnemyBase.Health.IsDeath())
            {
                enemy.SetEnemyBase(null);
                return;
            }

            Vector3 targetPos = enemy.EnemyBase.transform.position;
            targetPos.z = enemy.transform.position.z;

            enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, targetPos, enemy.Config.Speed * Time.deltaTime);
            enemy.transform.LookAt(enemy.EnemyBase.transform);

            Entity target = enemy.Search.FindTarget(enemy.transform.position, enemy.Config.ChaseDistance, 1 << enemy.EnemyLayer);

            if (target != null && !target.Health.IsDeath())
            {
                enemy.SetTarget(target);

                enemy.stateMachine.ChangeState(StateId.Chase);
            }
        }
        else
        {
            enemy.stateMachine.ChangeState(StateId.Idle);
        }

    }
}

