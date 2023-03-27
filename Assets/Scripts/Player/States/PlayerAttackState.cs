using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
[RequireComponent(typeof(Animator))]
public class PlayerAttackState : State
{
    private const string AnimatorAttack = "Attack";

    [SerializeField] private ThrowingSphere _sphere;
    [SerializeField] private Spawner _spawner;

    private Player _player;
    private Animator _animator;
    private Enemy _target;
    private float _delay;
    private float _time;

    private void Awake()
    {
        _player = GetComponent<Player>();
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        if (_target != null)
            _target.Dead += SetTarget;
        else
            SetTarget();

        _delay = 1 / _player.AttackSpeed;
        _time = _delay;
    }

    private void OnDisable()
    {
        if (_target != null)
            _target.Dead -= SetTarget;
    }

    private void Update()
    {
        _delay = 1 / _player.AttackSpeed;
        _time += Time.deltaTime;

        if (_time >= _delay)
        {
            if (_target != null)
            {
                _animator.Play(AnimatorAttack);
                transform.LookAt(_target.transform);
                ThrowingSphere sphere = Instantiate(_sphere, transform.position, Quaternion.identity);
                sphere.Launch(_target, _player.Damage);
                _time = 0;
            }
        }
    }

    private void SetTarget(Creature creature = null)
    {
        _target = _spawner.GetNearestEnemy(transform);

        if (_target == null)
            IsActive = false;
        else
            _target.Dead += SetTarget;
    }
}