using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField]
    private List<BaseEnemy> enemies;

    [SerializeField]
    private Transform content;

    [SerializeField]
    private ShopItem shopItemPrefab;

    [SerializeField]
    private Team userTeam = Team.Blue;

    [SerializeField]
    private Team botTeam = Team.Red;

    private void Awake()
    {
        if(enemies.Count > 0)
        {
            foreach(var enemy in enemies)
            {
                var item = Instantiate(shopItemPrefab, content);
                item.Init(enemy.Config.Price.ToString(), enemy.Config.Avatar, () => EnemySpawner.Singletion.Spawn(enemy, userTeam, botTeam));
            }
        }
    }
}
