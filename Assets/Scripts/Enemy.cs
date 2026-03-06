using UnityEngine;
using System.Collections.Generic;
using System;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _movementSpeed = 1f;
    [SerializeField] private int _health = 1;
    public int Reward = 30;
    public event Action<Enemy> OnDeath;
    public event Action<Enemy> OnGoalReached;

    private Transform _pathParent;
    private int _currentWaypoint = 0;
    private Vector3 randomOffset;

    public static readonly List<Enemy> ActiveEnemies = new();

    private void OnEnable()
    {
        ActiveEnemies.Add(this);
    }

    private void OnDisable()
    {
        ActiveEnemies.Remove(this);
    }

    public void Initialize(Transform pathParent)
    {
        _pathParent = pathParent;
        transform.position = pathParent.GetChild(0).position;
    }

    private void Update()
    {
        if (_pathParent != null)
        {
            if (_pathParent.childCount > _currentWaypoint)
                MoveAlongPath();
            else
                ReachGoal();

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

    public void Damage(int damage)
    {
        _health -= damage;
        if (_health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        OnDeath?.Invoke(this);
        gameObject.SetActive(false);
    }

    private void ReachGoal()
    {
        OnGoalReached?.Invoke(this);
        gameObject.SetActive(false);
    }
}
