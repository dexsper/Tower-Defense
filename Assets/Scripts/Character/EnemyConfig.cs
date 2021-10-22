using UnityEngine;

[CreateAssetMenu()]
public class EnemyConfig : ScriptableObject
{
    public float Speed = 2f;

    public float Health = 50f;

    public float Price = 10f;

    public float Damage = 15f;

    public float ChaseDistance = 10f;

    public float AttackDistance = 2f;
}