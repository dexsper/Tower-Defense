using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Singletion;

    private List<BaseEnemy> enemies = new List<BaseEnemy>();

    [SerializeField]
    private Base[] bases;


    private void Awake()
    {
        if (Singletion == null)
            Singletion = this;
        else Destroy(this);
    }


    public void Spawn(BaseEnemy e, Team team)
    {
        Base ourBase = bases.Where(b => b.Team == team).First();
        Base enemyBase = bases.Where(b => b.Team != team).First();

        if (ourBase.Health.IsDeath() || enemyBase.Health.IsDeath()) return;

        if (ourBase.Economic.TakeMoney(e.Config.Price))
        {
            Vector3 spawnPosition = ourBase.SpawnPoint.position;

            BaseEnemy enemy = Instantiate(e, spawnPosition, Quaternion.identity);

            enemy.Init(team, enemyBase, ourBase.EnemyLayer);

            enemies.Add(enemy);
        }

    }

 
}
