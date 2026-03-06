using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private int[] _enemiesPerWave;
    [SerializeField] private Vector2 _spawnInterval;
    [SerializeField] private PlayerEconomy _playerEconomy;
    [SerializeField] private UIManager _uiManager;

    private int _currentWave = 0;
    private Coroutine _waveCoroutine;


    private void Start()
    {
        _waveCoroutine = StartCoroutine(WaveCoroutine());
    }

    private void OnEnable()
    {
        _playerEconomy.OnAllHealthLost += LoseGame;
    }

    private IEnumerator WaveCoroutine()
    {
        while(_currentWave < _enemiesPerWave.Length)
        {
            Debug.Log("Wave " + _currentWave + " started");
            _enemySpawner.StartWave(_spawnInterval, _enemiesPerWave[_currentWave]);
            while (Enemy.ActiveEnemies.Count > 0)
            {
                yield return null;
            }
            _currentWave++;
        }
        GameOver(true);

    }

    private void LoseGame(int health)
    {
        GameOver(false);
    }

    private void GameOver(bool victory)
    {
        if (!victory)
        {
            if (_waveCoroutine != null)
                StopCoroutine(_waveCoroutine);
        }
        Time.timeScale = 0;
        _uiManager.ShowGameOver(victory);
    }

    public void ResetGame()
    {
        Debug.Log("Game reset");
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
