using UnityEngine;

class EnemyDeathState : State
{
    BaseEnemy enemy;
    public void Enter(BaseEnemy e)
    {
        enemy = e;

        e.Anim.SetTrigger("Death");

        e.animationEvents.OnAnimationEvent += HandleDeathEvent;
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

