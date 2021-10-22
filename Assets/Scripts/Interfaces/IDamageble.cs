using UnityEngine;

public interface IDamageble 
{
    public void TakeDamage(float damage);

    public GameObject GetObject();

    public bool IsDeath();
}

