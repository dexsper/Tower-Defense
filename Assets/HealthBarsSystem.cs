using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarsSystem : MonoBehaviour
{
    private Dictionary<Entity, HealthBar> healthBars = new Dictionary<Entity, HealthBar>();

    [SerializeField]
    private HealthBar healthBarPrefab;

    private void Awake()
    {
        Entity.OnEntityCreate += CreateHealthBar;
        Entity.OnEntityDestroy += RemoveHealthBar;
    }

    private void RemoveHealthBar(Entity entity)
    {
        if (!healthBars.ContainsKey(entity))
        {
            Destroy(healthBars[entity].gameObject);
            healthBars.Remove(entity);
        }
    }

    private void CreateHealthBar(Entity entity)
    {
        if(!healthBars.ContainsKey(entity))
        {
            var healthBar = Instantiate(healthBarPrefab, transform);
            healthBars.Add(entity, healthBar);
            healthBar.Init(entity);
        }
    }
}
