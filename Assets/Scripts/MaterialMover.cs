using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialMover : MonoBehaviour
{
    [SerializeField]
    private float scrollSpeed = 0.5f;
    [SerializeField]
    private Renderer renderer;

    private void Start()
    {
        renderer = GetComponent<Renderer>();
    }
    private void Update()
    {
        float offset = Time.time * scrollSpeed;
        renderer.material.SetTextureOffset("_MainTex", new Vector2(0, offset));
    }
}