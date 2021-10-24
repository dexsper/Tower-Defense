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

    private void Start()
    {
        OnDeath += DeathHandle;
    }

    private void DeathHandle()
    {
        Debug.Log($"The base of the {team} was destroyed");
    }
}
