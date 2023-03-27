using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class HealthBar : MonoBehaviour
{
    [SerializeField] private Creature _creature;
    [SerializeField] private Image _filled;
    [SerializeField] private Slider _slider;

    private void OnEnable()
    {
        _creature.HealthChanged += UpdateBar;
    }

    private void OnDisable()
    {
        _creature.HealthChanged -= UpdateBar;
    }

    private void Start()
    {
        UpdateBar(_creature.Health, _creature.MaxHealth);
    }

    private void UpdateBar(float health, float maxHealth)
    {
        if (health > 0)
            _slider.value = health / maxHealth;
        else
            Destroy(gameObject);
    }
}
