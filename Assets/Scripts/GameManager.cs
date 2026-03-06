using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private int[] enemiesPerWave;
    [SerializeField] private Vector2 spawnInterval;

    private int _currentWave = 0;
    private Coroutine _waveCoroutine;

    private void Start()
    {
        _waveCoroutine = StartCoroutine(WaveCoroutine());
    }

    private IEnumerator WaveCoroutine()
    {
        while(_currentWave < enemiesPerWave.Length)
        {
            Debug.Log("Wave " + _currentWave + " started");
            enemySpawner.StartWave(spawnInterval, enemiesPerWave[_currentWave]);
            while (Enemy.ActiveEnemies.Count > 0)
            {
                yield return null;
            }
            _currentWave++;
        }
        GameOver(true);

    }

    public void GameOver(bool victory)
    {
        if (!victory)
        {
            if (_waveCoroutine != null)
                StopCoroutine(_waveCoroutine);
        }
    }
}
