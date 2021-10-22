using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEnemy : MonoBehaviour, IDamageble
{
    [SerializeField]
    private StateId initialState;

    [SerializeField]
    private EnemyConfig config;

    [SerializeField]
    private Color attackZoneColor;

    [SerializeField]
    private LayerMask enemyLayer = default(LayerMask);

    private Base enemyBase;

    private IDamageble target;

    private float health;

    private Animator anim;

    public Animator Anim => anim;
    public Base EnemyBase => enemyBase;
    public IDamageble Target => target;
    public EnemyConfig Config => config;
    public StateMachine stateMachine { get; protected set; }
    public Team team { get; protected set; }

    public bool IsDeath() => health <= 0f;

    public LayerMask EnemyLayer => enemyLayer;


    private void Start()
    {
        anim = GetComponent<Animator>();

        health = config.Health;

        stateMachine = new StateMachine(this);
        stateMachine.RegisterState(new CharacterIdleState());
        stateMachine.RegisterState(new CharacterChaseState());
        stateMachine.RegisterState(new CharacterRunState());
        stateMachine.RegisterState(new CharacterAttackState());
        stateMachine.RegisterState(new CharacterDeathState());
        stateMachine.ChangeState(initialState);
    }

    public virtual void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            stateMachine.ChangeState(StateId.Death);
        }
    }

    public virtual bool HasTarget()
    {
        return target != null;
    }

    public virtual void SetTarget(IDamageble enemy)
    {
        target = enemy;
    }

    public virtual void Init(Team team, Base enemyBase)
    {
        this.team = team;
        this.enemyBase = enemyBase;
    }

    public virtual void OnAnimationIvent(string eventName)
    {
        switch(eventName)
        {
            case "Attack":
                {
                    target?.TakeDamage(config.Damage);
                    break;
                }
            case "Death":
                {
                    Destroy(gameObject);
                    break;
                }
        }
    }

    private void Update()
    {
        stateMachine.Update();
    }

    private void OnDrawGizmos()
    {
        if (config)
        {
            Gizmos.color = attackZoneColor;
            Gizmos.DrawSphere(transform.position, config.ChaseDistance);
        }
    }

    public GameObject GetObject()
    {
        return gameObject;
    }
}
