using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatData
{
    public float Value;
    public float CurrentValue;
    public int Level;
    public StatType StatType;
    public float EnhanceValueModifier;
    public int EnhancePrice;
    public float EnhancePriceModifier;

    public StatData(float value, float currentValue, int level, StatType statType, float enhanceValueModifier, int enhancePrice, float enhancePriceModifier)
    {
        Value = value;
        CurrentValue = currentValue;
        Level = level;
        StatType = statType;
        EnhanceValueModifier = enhanceValueModifier;
        EnhancePrice = enhancePrice;
        EnhancePriceModifier = enhancePriceModifier;
    }
}
