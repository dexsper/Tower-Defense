using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : BaseEnemy
{
    protected override void InitializeComponents()
    {
        base.InitializeComponents();


        stateMachine = new StateMachine(this);

        stateMachine.RegisterState(new EnemyIdleState());
        stateMachine.RegisterState(new EnemyChaseState());
        stateMachine.RegisterState(new EnemyRunState());
        stateMachine.RegisterState(new EnemyAttackState());
        stateMachine.RegisterState(new EnemyDeathState());

        stateMachine.ChangeState(initialState);

    }
}
