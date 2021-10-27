using System;
using UnityEngine;

public class EnemyAttackState : State
{
    BaseEnemy enemy;

    public void Enter(BaseEnemy e)
    {
        enemy = e;

        enemy.Anim.SetBool("Attack", true);
        enemy.animationEvents.OnAnimationEvent += HandleAttackEvent;
    }

    private void HandleAttackEvent(string name)
    {
        if(name == "Attack")
        {
            enemy.Target.Health.Damage(enemy.Config.Damage);
        }
    }

    public void Exit()
    {
        enemy.Anim.SetBool("Attack", false);
    }

    public StateId GetId()
    {
        return StateId.Attack;
    }

    public void Update()
    {
        if(enemy.Target.Health.IsDeath())
        {
            enemy.SetTarget(null);
        }

        if(enemy.Target == null)
        {
            enemy.stateMachine.ChangeState(StateId.Run);
        }

        else if (Vector3.Distance(enemy.transform.position, enemy.Target.transform.position) > enemy.Config.AttackDistance)
        {
            enemy.stateMachine.ChangeState(StateId.Chase);
        }

    }
}
