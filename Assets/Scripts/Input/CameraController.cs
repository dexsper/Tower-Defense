using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private float smothness = 0.08f;
    [SerializeField]
    private Vector2 horizontalLimit;

    private float dist;
        
    private Vector3 offset;

    float x;

    private void Start()
    {
        dist = transform.position.z;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            offset = MouseToWorld();

        }
        else if (Input.GetMouseButton(0))
        {
            x = (transform.position - (MouseToWorld() - offset)).x;
        }

        Vector3 newPos = transform.position;
        newPos.x = Mathf.Clamp(x, horizontalLimit.x, horizontalLimit.y);

        transform.position = Vector3.Lerp(transform.position, newPos, smothness);
    }

    private Vector3 MouseToWorld()
    {
        Vector3 pos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, dist);

        pos = Camera.main.ScreenToWorldPoint(pos);
        pos.z = transform.position.z;
        pos.y = transform.position.y;

        return pos;
    }
}
