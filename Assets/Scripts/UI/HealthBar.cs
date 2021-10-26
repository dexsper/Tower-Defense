using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private Slider slider;

    Camera cam;

    private Entity entity;

    

    private void Awake()
    {
        cam = Camera.main;
    }

    public void Init(Entity entity)
    {
        this.entity = entity;
        entity.Health.OnHealthChanged += UpdateBar;
        gameObject.SetActive(true);
        UpdatePosition();
    }

    public void UpdateBar(float health, float maxHealth)
    {
        if(slider)
            slider.value = health / maxHealth;
    }

    private void Update()
    {
        UpdatePosition();
    }

    private void UpdatePosition()
    {
        if (entity)
            transform.position = cam.WorldToScreenPoint(entity.transform.position + entity.Health.HealthBarOffset);
    }

    private void OnDestroy()
    {
        entity.Health.OnHealthChanged -= UpdateBar;
    }
}
