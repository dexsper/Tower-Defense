using System.Linq;
using UnityEngine;

public class EnemySearch : MonoBehaviour
{
    public Entity FindTarget(Vector3 position, float distance, LayerMask layer)
    {
        Collider[] colliders = Physics.OverlapSphere(position, distance, layer);

        Entity target = colliders.OrderBy(c => Vector3.Distance(position, c.transform.position))
            .FirstOrDefault()?.GetComponent<Entity>();

        return target;
    }
}
