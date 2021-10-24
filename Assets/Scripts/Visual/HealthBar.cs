using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private Slider slider;

    Camera cam;

    private Entity entity;

    

    private void Start()
    {
        cam = Camera.main;
    }

    public void Init(Entity entity)
    {
        this.entity = entity;
        entity.OnHealthChanged += UpdateBar;
    }

    public void UpdateBar(float health, float maxHealth)
    {
        if(slider)
            slider.value = health / maxHealth;
    }

    private void Update()
    {

        if (entity)
            transform.position = cam.WorldToScreenPoint(entity.transform.position + entity.HealthBarOffset);
    }

    private void OnDestroy()
    {
        entity.OnHealthChanged -= UpdateBar;
    }
}
