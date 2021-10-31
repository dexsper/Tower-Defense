using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Singletion;

    private List<BaseEnemy> enemies = new List<BaseEnemy>();


    private void Awake()
    {
        if (Singletion == null)
            Singletion = this;
        else Destroy(this);
    }


    public void Spawn(BaseEnemy e, Team team, Team enemyTeam)
    {
        Base base1 = Base.Bases[team];
        Base base2 = Base.Bases[enemyTeam];

        if (base1.Health.IsDeath() || base2.Health.IsDeath()) return;

        if (base1.Economic == null) 
            throw new MissingReferenceException("Base econimics not found!");

        if (base1.SpawnPoints == null || base1.SpawnPoints.Length == 0)
            throw new NullReferenceException("Spawn points doesn't exists!");

        if (base1.Economic.TakeMoney(e.Config.Price))
        {
            Vector3 spawnPosition = base1.SpawnPoints[Random.Range(0, base1.SpawnPoints.Length)].position;

            BaseEnemy enemy = Instantiate(e, spawnPosition, Quaternion.identity);

            enemy.Init(team, base2, base1.EnemyLayer);

            enemies.Add(enemy);
        }

    }

 
}
