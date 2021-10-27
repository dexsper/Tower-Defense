using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MortarAttackState : State
{
    BaseEnemy enemy;
    
    public void Enter(BaseEnemy e)
    {
        enemy = e;   
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
        
    }
}
