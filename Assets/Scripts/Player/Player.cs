using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Player : Creature
{
    public int Money { get; private set; }

    public event UnityAction<int> MoneyChanged;

    public void ReceiveMoney(int money)
    {
        Money += Mathf.Clamp(money, 0, int.MaxValue);
        MoneyChanged?.Invoke(money);
    }

    public void EnhanceStat(StatType statType)
    {
        Stat stat = Stats.FirstOrDefault(stat => stat.StatType == statType);

        if (stat != null)
        {
            if (stat.EnhancePrice <= Money)
            {
                Money -= stat.EnhancePrice;
                MoneyChanged?.Invoke(Money);
                stat.Enhance();
            }
        }
    }

    protected override void OnDie()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
