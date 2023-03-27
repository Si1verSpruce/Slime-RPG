using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerMoveState : State
{
    [SerializeField] private MeshRenderer _groundPlane;
    [SerializeField] private Spawner _spawner;

    private Player _player;
    private float _startPositionX;
    private float _endPositionX;

    private void Awake()
    {
        _player = GetComponent<Player>();

        _startPositionX = transform.position.x;
        _endPositionX = _startPositionX + _groundPlane.bounds.size.x;
    }

    private void OnEnable()
    {
        transform.LookAt(transform.position + Vector3.right);
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * _player.MoveSpeed * Time.deltaTime);

        if (transform.position.x >= _endPositionX)
        {
            if (_spawner.HasEnemies == false)
            {
                Vector3 currentOffset = transform.position - Camera.main.transform.position;
                transform.position = new Vector3(_startPositionX + transform.position.x - _endPositionX, transform.position.y, transform.position.z);
                Camera.main.transform.position = transform.position - currentOffset;
            }
        }
    }
}
