using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private PlayerEconomy _playerEconomy;
    [SerializeField] private GameManager _gameManager;

    [SerializeField] private TextMeshProUGUI _goldText;
    [SerializeField] private TextMeshProUGUI _livesText;

    [Header("Game Over UI")]
    [SerializeField] private GameObject _gameOverScreen;
    [SerializeField] private TextMeshProUGUI _gameOverText;
    [SerializeField] private Button _playAgainButton;

    private bool goldInitialized = false;
    private bool healthInitialized = false;

    private void OnEnable()
    {
        _playerEconomy.OnGoldChanged += UpdateGold;
        _playerEconomy.OnHealthChanged += UpdateLives;
    }

    private void OnDisable()
    {
        _playerEconomy.OnGoldChanged -= UpdateGold;
        _playerEconomy.OnHealthChanged -= UpdateLives;
    }

    private void Start()
    {
        _playAgainButton.onClick.AddListener(PlayAgainClick);
    }

    private void UpdateGold(int gold)
    {
        _goldText.text = gold.ToString();
        if (goldInitialized)
            _goldText.GetComponent<Animator>().Play("ButtonScale");
        else
            goldInitialized = true;
    }

    private void UpdateLives(int lives)
    {
        _livesText.text = lives.ToString();
        if (healthInitialized)
            _livesText.GetComponent<Animator>().Play("ButtonScale");
        else
            healthInitialized = true;
    }

    public void ShowGameOver(bool win)
    {
        if (win)
            _gameOverText.text = "YOU WIN";
        else
            _gameOverText.text = "YOU LOSE";
        _gameOverScreen.SetActive(true);
        Canvas.ForceUpdateCanvases();
        LayoutRebuilder.ForceRebuildLayoutImmediate(_gameOverScreen.GetComponent<RectTransform>());
    }

    private void PlayAgainClick()
    {
        _gameManager.ResetGame();
    }

}
