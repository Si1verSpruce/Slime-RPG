using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyDistanceTransition : Transition
{
    [SerializeField] private float _distanceToTransit;

    private Enemy _enemy;

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, _enemy.Target.transform.position) <= _distanceToTransit)
            NeedTransit = true;
    }
}
