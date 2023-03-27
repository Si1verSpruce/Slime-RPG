using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMoveState : State
{
    private Enemy _enemy;

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _enemy.Target.transform.position, _enemy.MoveSpeed * Time.deltaTime);
        transform.LookAt(_enemy.Target.transform);
    }
}
