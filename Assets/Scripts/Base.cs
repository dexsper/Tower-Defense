using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Team
{
    Blue,
    Red
}

public class Base : MonoBehaviour, IDamageble
{

    [SerializeField]
    private float health = 100;

    [SerializeField]
    private Team team;

    [SerializeField]
    private float money;

    [SerializeField]
    private Transform spawnPoint;

    public Transform SpawnPoint => spawnPoint;

    public float Money => money;
    public Team Team => team;

    EnemySpawner spawner;
    public bool IsDeath() => health <= 0f;

    private void Start()
    {
        spawner = GetComponent<EnemySpawner>();

    }
    [ContextMenu("Test Spawn")]
    public void TestSpawn()
    {
        spawner.Spawn(0, team);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        if(health <= 0 )
        {
            Debug.Log(team + " base was destroyed");
        }
    }

    public GameObject GetObject()
    {
        return gameObject;
    }
}
