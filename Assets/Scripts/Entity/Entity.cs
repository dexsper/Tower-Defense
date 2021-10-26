using System;
using UnityEngine;

[RequireComponent(typeof(Health))]
public abstract class Entity : MonoBehaviour
{
    public static event Action<Entity> OnEntityCreate = delegate { };

    public static event Action<Entity> OnEntityDestroy = delegate { };

    public Health Health { get; protected set; }

    private void Awake()
    {
        InitializeComponents();
    }

    protected virtual void InitializeComponents()
    { 
        Health = GetComponent<Health>();

        OnEntityCreate(this);
    }

    private void OnDestroy()
    {
        OnEntityDestroy(this);
    }
}

