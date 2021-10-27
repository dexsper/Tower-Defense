using UnityEngine;

[CreateAssetMenu()]
public class EnemyConfig : ScriptableObject
{
    public float Speed = 2f;

    public int Price = 10;

    public float Damage = 15f;

    public float ChaseDistance = 10f;

    public float AttackDistance = 2f;

    public Sprite Avatar;
}