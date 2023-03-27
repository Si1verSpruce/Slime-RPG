using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public abstract class Creature : MonoBehaviour
{
    [SerializeField] private StatParameters _healthParameters;
    [SerializeField] private StatParameters _moveSpeedParameters;
    [SerializeField] private StatParameters _damageParameters;
    [SerializeField] private StatParameters _attackSpeedParameters;
    [SerializeField] protected TextMeshProUGUI FloatingText;
    [SerializeField] protected RectTransform FloatingTextContainer;
    [SerializeField] private Vector3 _floatingTextOffset;
    [SerializeField] private float _floatingTextFloatDistance;
    [SerializeField] private float _floatingTextFloatDuration;

    protected List<Stat> Stats = new List<Stat>();
    private Health _health;
    private Stat _moveSpeed;
    private Stat _damage;
    private Stat _attackSpeed;

    public float MaxHealth => _health.Value;
    public float Health => _health.CurrentValue;
    public float MoveSpeed => _moveSpeed.CurrentValue;
    public float Damage => _damage.CurrentValue;
    public float AttackSpeed => _attackSpeed.CurrentValue;

    public event UnityAction<float, float> HealthChanged;
    public event UnityAction<Creature> Dead;

    protected virtual void Awake()
    {
        Stats.Add(_health = new Health(_healthParameters));
        Stats.Add(_moveSpeed = new Stat(_moveSpeedParameters));
        Stats.Add(_damage = new Stat(_damageParameters));
        Stats.Add(_attackSpeed = new Stat(_attackSpeedParameters));
    }

    protected virtual void OnEnable()
    {
        _health.ValueChanged += OnHealthChanged;
    }
    
    protected virtual void OnDisable()
    {
        _health.ValueChanged -= OnHealthChanged;
    }

    public void ApplyDamage(float damage)
    {
        _health.ApplyDamage(CalculateDamage(damage));
        HealthChanged?.Invoke(_health.CurrentValue, _health.Value);
        GameObject floatingText = Instantiate(FloatingText.gameObject, transform.position + _floatingTextOffset, Quaternion.Euler(Vector3.right + Camera.main.transform.rotation.eulerAngles), FloatingTextContainer);
        floatingText.GetComponent<TextMeshProUGUI>().text = damage.ToString();
        floatingText.transform.DOMove(floatingText.transform.position + Vector3.up * _floatingTextFloatDistance, _floatingTextFloatDuration).OnComplete(() => Destroy(floatingText));

        if (_health.CurrentValue <= 0)
            Die();
    }

    public StatData GetStatData(StatType statType)
    {
        Stat stat = Stats.FirstOrDefault(stat => stat.StatType == statType);

        return stat.GetData();
    }

    protected virtual void OnDie() { }

    protected virtual float CalculateDamage(float damage)
    {
        return damage;
    }

    private void OnHealthChanged()
    {
        HealthChanged?.Invoke(_health.CurrentValue, _health.Value);
    }

    private void Die()
    {
        Dead?.Invoke(this);
        OnDie();
    }
}
