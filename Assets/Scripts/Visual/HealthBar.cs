using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private Slider slider;

    Camera cam;

    private void Start()
    {
        cam = Camera.main;
    }

    public void UpdateBar(float health, float maxHealth)
    {
        if(slider)
            slider.value = health / maxHealth;
    }

    private void Update()
    {
        if(slider)
            slider.transform.LookAt(cam.transform);
    }
}
