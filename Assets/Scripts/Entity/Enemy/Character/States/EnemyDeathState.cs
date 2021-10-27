using UnityEngine;

class EnemyDeathState : State
{
    BaseEnemy enemy;
    public void Enter(BaseEnemy e)
    {
        enemy = e;

        enemy.Anim.SetTrigger("Death");

        enemy.animationEvents.OnAnimationEvent += HandleDeathEvent;
    }

    private void HandleDeathEvent(string name)
    {
        if (name == "Death")
        {
           Object.Destroy(enemy.gameObject);
        }
    }

    public void Exit()
    {
    }

    public StateId GetId()
    {
        return StateId.Death;
    }

    public void Update()
    {
        
    }
}

