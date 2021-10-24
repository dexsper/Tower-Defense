using System;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    [SerializeField]
    protected float maxHealth;

    protected float health;

    public event Action<float, float> OnHealthChanged = delegate { };

    public static event Action<Entity> OnEntityCreate = delegate { };

    public static event Action<Entity> OnEntityDestroy = delegate { };

    public event Action OnDeath = delegate { };

    [SerializeField]
    private Vector3 healthBarOffset;

    public Vector3 HealthBarOffset => healthBarOffset;

    private void Awake()
    {
        InitializeComponents();
    }

    protected virtual void InitializeComponents()
    {
        if (maxHealth == 0)
            maxHealth = 100f;

        health = maxHealth;


        OnEntityCreate(this);
    }

    public virtual void ModifyHealth(float amount)
    {
        health -= amount;

        OnHealthChanged(health, maxHealth);

        if (health <= 0)
            OnDeath();
    }

    public virtual bool IsDeath()
    {
        return health <= 0;
    }

    private void OnDestroy()
    {
        OnEntityDestroy(this);
    }
}

