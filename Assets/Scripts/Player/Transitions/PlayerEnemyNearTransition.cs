using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnemyNearTransition : Transition
{
    [SerializeField] private Spawner _enemySpawner;
    [SerializeField] private float _transitDistance;

    private void OnEnable()
    {
        _enemySpawner.WaveSpawned += OnWaveSpawned;
    }

    private void OnDisable()
    {
        _enemySpawner.WaveSpawned -= OnWaveSpawned;
    }

    private void OnWaveSpawned()
    {
        StartCoroutine(TransitWhenEnemyNear(_enemySpawner.GetNearestEnemy(transform)));
    }

    private IEnumerator TransitWhenEnemyNear(Enemy enemy)
    {
        yield return new WaitUntil(() => Vector3.Distance(transform.position, enemy.transform.position) <= _transitDistance);

        NeedTransit = true;
    }
}
