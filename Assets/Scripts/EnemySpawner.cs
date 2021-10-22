using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private List<BaseEnemy> enemies;

    private Base[] bases;
    

    private void Start()
    {
        bases = FindObjectsOfType<Base>();
    }

    public void Spawn(int index, Team team)
    {
        if (enemies == null || enemies.Count == 0) return;

        Base ourBase = bases.Where(b => b.Team == team).First();
        Base enemyBase = bases.Where(b => b.Team != team).First();

        BaseEnemy enemy = Instantiate(enemies[index], ourBase.SpawnPoint.position, Quaternion.identity);
        enemy.Init(team, enemyBase);

        enemies.Add(enemy);
    }

 
}
