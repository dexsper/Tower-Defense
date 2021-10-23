using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemySearch))]
public abstract class BaseEnemy : Entity
{
    [SerializeField]
    protected StateId initialState;

    [SerializeField]
    protected EnemyConfig config;

    [SerializeField]
    protected Color attackZoneColor;

    [SerializeField]
    protected LayerMask enemyLayer = default(LayerMask);

    protected Base enemyBase;

    protected Entity target;

    protected Animator anim;

    public EnemySearch Search { get; protected set; }

    public Animator Anim => anim;
    public Base EnemyBase => enemyBase;
    public Entity Target => target;
    public EnemyConfig Config => config;
    public StateMachine stateMachine { get; protected set; }
    public Team team { get; protected set; }

    public LayerMask EnemyLayer => enemyLayer;

    HealthBar bar;


    public override void TakeDamage(float damage)
    {
        health -= damage;

        bar.UpdateBar(health, maxHealth);

        if (health <= 0)
        {
            stateMachine.ChangeState(StateId.Death);
        }
    }

    public virtual bool HasTarget()
    {
        return target != null;
    }

    public virtual void SetTarget(Entity enemy)
    {
        target = enemy;
    }

    public void SetEnemyBase(Base enemyBase)
    {
        this.enemyBase = enemyBase;
    }
    
    protected override void InitializeComponents()
    {
        anim = GetComponent<Animator>();
        bar = GetComponent<HealthBar>();
        Search = GetComponent<EnemySearch>();

        maxHealth = config.Health;
        health = maxHealth;

        stateMachine = new StateMachine(this);
        stateMachine.RegisterState(new CharacterIdleState());
        stateMachine.RegisterState(new CharacterChaseState());
        stateMachine.RegisterState(new CharacterRunState());
        stateMachine.RegisterState(new CharacterAttackState());
        stateMachine.RegisterState(new CharacterDeathState());
        stateMachine.ChangeState(initialState);
    }

    public virtual void Init(Team team, Base enemyBase, LayerMask enemyLayer)
    {
        this.team = team;
        this.enemyBase = enemyBase;
        this.enemyLayer = enemyLayer;

        gameObject.layer = LayerMask.NameToLayer(team.ToString());
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

    protected virtual void OnDrawGizmos()
    {
        if (config)
        {
            Gizmos.color = attackZoneColor;
            Gizmos.DrawSphere(transform.position, config.ChaseDistance);
        }
    }
}
