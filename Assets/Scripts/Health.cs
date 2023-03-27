using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : Stat
{
    public Health(float value, StatType type, int enhancePrice, int level = 0, float enhanceValueModifier = 1, float enhancePriceModifier = 1) 
        : base(value, type, enhancePrice, level, enhanceValueModifier, enhancePriceModifier) { }

    public Health(StatParameters parameters) : base(parameters) { }

    public void ApplyDamage(float damage)
    {
        CurrentValue -= Mathf.Clamp(damage, 0, float.MaxValue);
    }
}
