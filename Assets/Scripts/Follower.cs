using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    [SerializeField] private Transform _followed;
    [SerializeField] private Vector3 _offset;

    private void LateUpdate()
    {
        transform.position = _followed.position + _offset;
    }
}
