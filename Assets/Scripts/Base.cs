using System;
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
    private int startMoney = 100;

    [SerializeField]
    private Transform spawnPoint;

    public Transform SpawnPoint => spawnPoint;

    public int Money { get; protected set; } = 0;
    public Team Team => team;

    [SerializeField]
    private LayerMask enemyLayer;

    public LayerMask EnemyLayer => enemyLayer;

    public static event Action<Team, int> OnMoneyChanged = delegate { };

    protected override void InitializeComponents()
    {
        base.InitializeComponents();

        OnDeath += DeathHandle;

        AddMoney(startMoney);
    }

    private void DeathHandle()
    {
        Debug.Log($"The base of the {team} was destroyed");
    }

    public void AddMoney(int amount)
    {
        Money += amount;
        OnMoneyChanged(team, Money);
    }

    public bool TakeMoney(int amount)
    {
        if (Money >= amount)
        {
            Money -= amount;
            OnMoneyChanged(team, Money);

            return true;
        }
        else return false;
    }
}
