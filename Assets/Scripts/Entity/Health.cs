using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    private Vector3 healthBarOffset;
    public Vector3 HealthBarOffset => healthBarOffset;

    [SerializeField]
    protected float maxHealth;

    protected float health;

    public event Action OnDeath = delegate { };

    public event Action<float, float> OnHealthChanged = delegate { };

    private bool isDeath = false;

    private void Awake()
    {
        if (maxHealth == 0)
            maxHealth = 100f;

        health = maxHealth;
    }

    public virtual void Damage(float amount)
    {
        if (isDeath) return;

        health -= amount;

        OnHealthChanged(health, maxHealth);

        if (health <= 0)
        {
            OnDeath();
            isDeath = true;
            GetComponent<Entity>().enabled = false;
        }
    }

    public virtual bool IsDeath()
    {
        return isDeath;
    }
}
