using System.Linq;
using UnityEngine;

class EnemyChaseState : State
{
    BaseEnemy enemy;

    public void Enter(BaseEnemy e)
    {
        enemy = e;
        enemy.Anim.SetBool("Run", true);
    }

    public void Exit()
    {
        enemy.Anim.SetBool("Run", false);
    }

    public StateId GetId()
    {
        return StateId.Chase;
    }

    public void Update()
    {
        if (enemy.Target == null)
        {
            enemy.stateMachine.ChangeState(StateId.Run);
        }

        float distance = Vector3.Distance(enemy.transform.position, enemy.Target.transform.position);

        if (distance < enemy.Config.AttackDistance + Random.Range(-2f, 2f))
        {
            enemy.stateMachine.ChangeState(StateId.Attack);
        }

        if (distance > enemy.Config.ChaseDistance)
        {
            enemy.SetTarget(null);
            enemy.stateMachine.ChangeState(StateId.Run);
            return;
        }
        else if (enemy.Target)
        {
            enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, enemy.Target.transform.position, enemy.Config.Speed * Time.deltaTime);
            enemy.transform.LookAt(enemy.Target.transform);
        }
    }
}

