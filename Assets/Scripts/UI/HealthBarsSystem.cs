using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarsSystem : MonoBehaviour
{
    private Dictionary<Entity, HealthBar> healthBars = new Dictionary<Entity, HealthBar>();

    [SerializeField]
    private HealthBar healthBarPrefab;

    [SerializeField]
    private Transform healthBarsParent;

    private void Awake()
    {
        Entity.OnEntityDestroy += RemoveHealthBar;
        Entity.OnEntityCreate += CreateHealthBar;
    }

    private void RemoveHealthBar(Entity entity)
    {
        if (healthBars.ContainsKey(entity))
        {
            var healthBar = healthBars[entity];

            if (healthBar)
                Destroy(healthBar.gameObject);

            healthBars.Remove(entity);
        }
    }

    private void CreateHealthBar(Entity entity)
    {
        if (healthBarPrefab == null)
            throw new NullReferenceException("Health bar prefab not found!");

        if (healthBarsParent == null)
            throw new NullReferenceException("Health bar parent not found!");

        if (!healthBars.ContainsKey(entity))
        {
            var healthBar = Instantiate(healthBarPrefab, healthBarsParent);
            healthBars.Add(entity, healthBar);
            healthBar.Init(entity);
        }
    }
}
