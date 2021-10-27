using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBot : MonoBehaviour
{
    [SerializeField]
    private Team team;

    [SerializeField]
    private List<BaseEnemy> enemies;

    [SerializeField]
    private float delay = 1.5f;

    private void Start()
    {
        StartCoroutine(TestSpawn());
    }

   private IEnumerator TestSpawn()
    {
        foreach (var enemy in enemies)
        {
            EnemySpawner.Singletion.Spawn(enemy, team);
            yield return new WaitForSeconds(delay);
        }
    }
}
