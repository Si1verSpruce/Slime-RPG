using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Enemy : Creature
{
    [SerializeField] private HealthBar _healthBar;
    [SerializeField] private int _money;
    [SerializeField] private StatType _scaledStat;

    private Player _target;

    public Player Target => _target;

    public void Init(Player player, RectTransform worldSpaceContainer)
    {
        _target = player;
        _healthBar.GetComponent<RectTransform>().SetParent(worldSpaceContainer);
        FloatingTextContainer = worldSpaceContainer;
    }

    public void EnhanceStat(int enhanceCount)
    {
        Stat stat = Stats.FirstOrDefault(stat => stat.StatType == _scaledStat);

        if (stat != null)
            stat.Enhance(enhanceCount);
    }

    protected override void OnDie()
    {
        _target.ReceiveMoney(_money);
        Destroy(gameObject);
    }
}
