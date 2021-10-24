using UnityEngine;

public class MaterialMover : MonoBehaviour
{
    [SerializeField]
    private float scrollSpeed = 0.5f;
    [SerializeField]
    private Renderer rend;

    private void Start()
    {
        rend = GetComponent<Renderer>();
    }

    private void Update()
    {
        float offset = Time.time * scrollSpeed;
        rend.material.SetTextureOffset("_MainTex", new Vector2(0, offset));
    }
}