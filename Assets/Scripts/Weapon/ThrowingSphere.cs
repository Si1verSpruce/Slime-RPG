using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingSphere : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _collisionDistance;
    [SerializeField] private float _jumpPower;
    [SerializeField] private int _numberJumps;

    public void Launch(Creature creature, float damage)
    {
        float launchDuration = Vector3.Distance(transform.position, creature.transform.position) / _moveSpeed;
        Vector3 predictedPosition = creature.transform.position + creature.MoveSpeed * creature.transform.forward * launchDuration;
        transform.DOJump(predictedPosition, _jumpPower, _numberJumps, launchDuration).SetEase(Ease.Linear).OnComplete(() => OnTargetReached(creature, damage));
    }

    private void OnTargetReached(Creature creature, float damage)
    {
        if (creature != null)
            creature.ApplyDamage(damage);

        Destroy(gameObject);
    }
}
