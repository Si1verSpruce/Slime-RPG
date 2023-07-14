using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum StatType
{
    Health,
    MoveSpeed,
    Damage,
    AttackSpeed
}

public class Stat
{
    protected float DefaultValue;
    protected int DefaultEnhancePrice;

    public float Value { get; private set; }
    public float CurrentValue { get; protected set; }
    public int Level { get; private set; }
    public StatType StatType { get; private set; }
    public float EnhanceValueModifier { get; private set; }
    public int EnhancePrice { get; private set; }
    public float EnhancePriceModifier { get; private set; }

    public event UnityAction ValueChanged;

    public Stat(float value, StatType statType, int enhancePrice, int level = 1, float enhanceValueModifier = 1, float enhancePriceModifier = 1)
    {
        Value = value;
        CurrentValue = value;
        Level = Mathf.Clamp(level, 1, int.MaxValue);
        StatType = statType;
        EnhancePrice = enhancePrice;
        EnhanceValueModifier = enhanceValueModifier;
        EnhancePriceModifier = enhancePriceModifier;

        DefaultValue = value;
        DefaultEnhancePrice = enhancePrice;
    }

    public Stat(StatParameters parameters) : this(parameters.Value, parameters.StatType, parameters.EnhancePrice,
        parameters.Level, parameters.EnhanceValueModifier, parameters.EnhancePriceModifier) { }

    public void Enhance(int enhanceCount = 1)
    {
        float oldValue = Value;
        Level += enhanceCount;
        Value = CalculateEnhancedValue();
        EnhancePrice = CalculateEnhancedPrice();
        OnValueEnhanced(oldValue, Value);
        ValueChanged?.Invoke();
    }

    public StatData GetData()
    {
        return new StatData(Value, CurrentValue, Level, StatType, EnhanceValueModifier, EnhancePrice, EnhancePriceModifier);
    }

    protected virtual void OnValueEnhanced(float oldValue, float value)
    {
        CurrentValue += value - oldValue;
    }

    protected virtual float CalculateEnhancedValue()
    {
        return DefaultValue + DefaultValue * EnhanceValueModifier * (Level - 1);
    }

    protected virtual int CalculateEnhancedPrice()
    {
        return Convert.ToInt32(DefaultEnhancePrice + DefaultEnhancePrice * EnhancePriceModifier * (Level - 1));
    }
}
