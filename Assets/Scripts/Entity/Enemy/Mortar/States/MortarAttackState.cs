
using UnityEngine;

public class MortarAttackState : State
{
    Mortar enemy;

    float launchProgress;

    public void Enter(BaseEnemy e)
    {
        enemy = e as Mortar;
    }

    public void Exit()
    {
       
    }

    public StateId GetId()
    {
        return StateId.Attack;
    }

    public void Update()
    {

        if (enemy.Target == null)
        {
            enemy.stateMachine.ChangeState(StateId.Run);
        }

        if (enemy.Target.Health.IsDeath())
        {
            enemy.SetTarget(null);
        }
        else if (Vector3.Distance(enemy.transform.position, enemy.Target.transform.position) > enemy.Config.AttackDistance)
        {
            enemy.SetTarget(null);
            enemy.stateMachine.ChangeState(StateId.Run);
        }

        enemy.transform.LookAt(enemy.Target.transform);

        if(enemy.canFire)
        {
            launchProgress += enemy.ShotsPerSeconds * Time.deltaTime;

            if (launchProgress >= 1f)
            {
                launchProgress = 0f;
                enemy.StartCoroutine(enemy.Fire());
            }
        }
    }
}
