
using UnityEngine;

public class MortarAttackState : State
{
    Mortar enemy;

    float launchProgress;

    public void Enter(BaseEnemy e)
    {
        enemy = e as Mortar;

        CheckTarget();
    }

    private void RemoveTarget()
    {
        launchProgress = 0f;
        enemy.SetTarget(null);
        enemy.stateMachine.ChangeState(StateId.Run);
    }

    private void CheckTarget()
    {
        if (enemy.Target == null)
        {
            enemy.stateMachine.ChangeState(StateId.Run);
        }

        else if (Vector3.Distance(enemy.transform.position, enemy.Target.transform.position) > enemy.Config.AttackDistance || enemy.Target.Health.IsDeath())
        {
            RemoveTarget();
        }
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
        CheckTarget();

        if (enemy?.Target)
        {
            Vector3 relativePos = enemy.Target.transform.position - enemy.transform.position;
            Quaternion rotation = Quaternion.LookRotation(new Vector3(relativePos.x, 0f, relativePos.z));

            enemy.transform.rotation = rotation;

            if (enemy.canFire)
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
}
