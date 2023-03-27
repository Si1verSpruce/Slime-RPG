using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
[RequireComponent(typeof(Animator))]
public class EnemyAttackState : State
{
    private const string AnimatorAttack = "Attack";

    private Enemy _enemy;
    private Animator _animator;
    private float _delay;
    private float _time;

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _delay = 1 / _enemy.AttackSpeed;
        _time += _delay;
    }

    private void Update()
    {
        _delay = 1 / _enemy.AttackSpeed;
        _time += Time.deltaTime;

        if (_time >= _delay)
        {
            if (_enemy.Target != null)
            {
                _animator.Play(AnimatorAttack);
                _enemy.Target.ApplyDamage(_enemy.Damage);
                _time = 0;
            }
        }
    }
}
