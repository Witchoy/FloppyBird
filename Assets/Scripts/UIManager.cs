using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private const int ARGENT = 5;
    private const int OR = 7;

    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject titleUI;
    [SerializeField] private GameObject scoreUI;

    [SerializeField] private GameObject medallionArgent;
    [SerializeField] private GameObject medallionOr;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text cardScoreText;
    [SerializeField] private TMP_Text cardHighScoreText;

    private void Start()
    {
        gameOverUI.SetActive(false);
        scoreUI.SetActive(false);
        titleUI.SetActive(true);

        GameManager.Instance.OnScoreChanged += UpdateScore;
        GameManager.Instance.OnStartGame += StartGame;
        GameManager.Instance.OnGameOver += GameOver;
    }

    private void OnDisable()
    {
        if (GameManager.Instance == null) return;

        GameManager.Instance.OnScoreChanged -= UpdateScore;
        GameManager.Instance.OnStartGame -= StartGame;
        GameManager.Instance.OnGameOver -= GameOver;
    }

    private void UpdateScore(int newScore)
    {
        scoreText.text = newScore.ToString();
    }

    private void StartGame()
    {
        Debug.Log("[UIManager] Start game");
        gameOverUI.SetActive(false);
        titleUI.SetActive(false);
        scoreUI.SetActive(true);
    }

    private void GameOver(int finalScore, int highScore)
    {
        Debug.Log("[UIManager] Game over");
        gameOverUI.SetActive(true);

        cardScoreText.text = finalScore.ToString();
        cardHighScoreText.text = highScore.ToString();

        if (finalScore >= OR)
        {
            medallionArgent.SetActive(false);
            medallionOr.SetActive(true);
        }
        else if (finalScore >= ARGENT)
        {
            medallionArgent.SetActive(true);
            medallionOr.SetActive(false);
        }
        else
        {
            medallionArgent.SetActive(false);
            medallionOr.SetActive(false);
        }
    }
}