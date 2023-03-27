using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private Transform _followed;

    private Vector3 _offset;

    private void OnEnable()
    {
        _offset = transform.position - _followed.position;
    }

    private void LateUpdate()
    {
        transform.position = _followed.position + _offset;
    }
}
