using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Team
{
    Blue,
    Red
}
[RequireComponent(typeof(Economic))]
public class Base : Entity
{
    [SerializeField]
    private Team team;

    [SerializeField]
    private Transform spawnPoint;

    public Transform SpawnPoint => spawnPoint;

    public Team Team => team;

    [SerializeField]
    private int enemyLayer;

    public int EnemyLayer => enemyLayer;

    public Economic Economic { get; protected set; }

    protected override void InitializeComponents()
    {
        base.InitializeComponents();

        Economic = GetComponent<Economic>();
    }
}
