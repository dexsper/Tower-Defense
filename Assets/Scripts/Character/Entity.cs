using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    [SerializeField]
    protected float health;

    public float maxHealth { get; protected set; }


    private void Start()
    {
        InitializeComponents();
    }

    protected abstract void InitializeComponents();

    public virtual void TakeDamage(float damage)
    {
        health -= damage;
    }

    public virtual bool IsDeath()
    {
        return health <= 0;
    }
}

