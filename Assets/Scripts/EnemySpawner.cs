using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private Transform pathParent;
    private Vector2 _spawnInterval;
    private int _enemiesPerWave;
    private Coroutine _spawningCoroutine;

    void Start()
    {
        StartWave(_spawnInterval, _enemiesPerWave);
    }

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
            GameObject enemy = Instantiate(_enemyPrefab, transform);
            enemy.GetComponent<Enemy>().Initialize(pathParent);
            yield return new WaitForSeconds(Random.Range(_spawnInterval[0], _spawnInterval[1]));

            _spawningCoroutine = null;
        }
    }
}
