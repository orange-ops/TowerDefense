using UnityEngine;
using System.Collections.Generic;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _movementSpeed = 1f;
    private Transform _pathParent;
    private int _currentWaypoint = 0;

    public void Initialize(Transform pathParent)
    {
        _pathParent = pathParent;
        transform.position = pathParent.GetChild(0).position;
    }

    private void Update()
    {
        if (_pathParent != null && _pathParent.childCount > _currentWaypoint)
        {
            MoveAlongPath();
        }
    }

    private void MoveAlongPath()
    {
        Vector3 targetPosition = _pathParent.GetChild(_currentWaypoint).position;
        Vector3 movementDirection = (targetPosition - transform.position).normalized;

        transform.position += movementDirection * _movementSpeed * Time.deltaTime;

        if ((targetPosition - transform.position).sqrMagnitude < 0.01f) {
            _currentWaypoint++;
        }
    }
}
