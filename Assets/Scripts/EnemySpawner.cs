using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private Transform _pathParent;
    [SerializeField] private PlayerEconomy _playerEconomy;

    private Vector2 _spawnInterval;
    private int _enemiesPerWave;
    private Coroutine _spawningCoroutine;

    public void StartWave(Vector2 spawnInterval, int enemiesPerWave)
    {
        _spawnInterval = spawnInterval;
        _enemiesPerWave = enemiesPerWave;
        if (_spawningCoroutine != null)
            StopCoroutine(_spawningCoroutine);
        _spawningCoroutine = StartCoroutine(SpawningCoroutine());
    }

    private IEnumerator SpawningCoroutine()
    {
        for (int i=0; i<_enemiesPerWave; i++)
        {
            GameObject enemyGO = Instantiate(_enemyPrefab, transform);
            Enemy enemy = enemyGO.GetComponent<Enemy>();
            enemy.Initialize(_pathParent);
            enemy.OnDeath += OnEnemyDeath;
            enemy.OnGoalReached += OnEnemyReachedGoal;
            yield return new WaitForSeconds(Random.Range(_spawnInterval[0], _spawnInterval[1]));

            _spawningCoroutine = null;
        }
    }

    private void OnEnemyDeath(Enemy enemy)
    {
        _playerEconomy.AddGold(enemy.Reward);
        enemy.OnDeath -= OnEnemyDeath;
    }

    private void OnEnemyReachedGoal(Enemy enemy)
    {
        _playerEconomy.LoseHealth(1);
        enemy.OnGoalReached -= OnEnemyReachedGoal;
    }
}
