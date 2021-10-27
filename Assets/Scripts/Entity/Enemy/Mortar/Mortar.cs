using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mortar : BaseEnemy
{
    protected override void InitializeComponents()
    {
        base.InitializeComponents();

        stateMachine = new StateMachine(this);

        stateMachine.RegisterState(new EnemyIdleState());
        stateMachine.RegisterState(new EnemyChaseState());
        stateMachine.RegisterState(new EnemyRunState());
        stateMachine.RegisterState(new MortarAttackState());
        stateMachine.RegisterState(new EnemyDeathState());

        stateMachine.ChangeState(initialState);
    }
}
