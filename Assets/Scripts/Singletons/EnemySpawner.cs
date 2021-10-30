using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


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
        Base userBase = Base.Bases[team];
        Base botBase = Base.Bases[enemyTeam];

        if (userBase.Health.IsDeath() || botBase.Health.IsDeath()) return;

        if (userBase.Economic.TakeMoney(e.Config.Price))
        {
            
            Vector3 spawnPosition = userBase.SpawnPoints[Random.Range(0, userBase.SpawnPoints.Length)].position;

            BaseEnemy enemy = Instantiate(e, spawnPosition, Quaternion.identity);

            enemy.Init(team, botBase, userBase.EnemyLayer);

            enemies.Add(enemy);
        }

    }

 
}
