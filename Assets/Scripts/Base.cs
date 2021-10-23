using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Team
{
    Blue,
    Red
}

public class Base : Entity
{
    [SerializeField]
    private Team team;

    [SerializeField]
    private float money;

    [SerializeField]
    private Transform spawnPoint;

    public Transform SpawnPoint => spawnPoint;

    public float Money => money;
    public Team Team => team;

    [SerializeField]
    private LayerMask enemyLayer;

    public LayerMask EnemyLayer => enemyLayer;

    HealthBar bar;

    [ContextMenu("Test Spawn")]
    public void TestSpawn()
    {
        EnemySpawner.Singletion.Spawn(0, team);
    }

    protected override void InitializeComponents()
    {
        maxHealth = health;
        bar = GetComponent<HealthBar>();
    }

    public override void TakeDamage(float damage)
    {
        health -= damage;

        bar.UpdateBar(health, maxHealth);

        if(IsDeath())
        {
            Debug.Log(team + " base was destroyed");
        }
    }
}
