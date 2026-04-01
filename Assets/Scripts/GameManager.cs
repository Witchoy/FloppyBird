using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private const int ARGENT = 5;
    private const int OR = 7;
    private const string HighScoreKey = "HighScore";
    private const string ScoreKey = "Score";
    public static GameManager Instance;

    private static bool _startOnLoad;

    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject titleUI;
    [SerializeField] private GameObject cardUI;
    [SerializeField] private GameObject scoreUI;

    [SerializeField] private GameObject player;

    [SerializeField] private GameObject pipeSpawner;
    [SerializeField] private LoopGround ground;

    [SerializeField] private GameObject medallionArgent;
    [SerializeField] private GameObject medallionOr;
    [SerializeField] private TextMeshProUGUI cardScoreText;
    [SerializeField] private TextMeshProUGUI cardHighScoreText;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        if (_startOnLoad)
        {
            _startOnLoad = false;
            BeginGame();
        }
        else
        {
            ShowTitle();
        }
    }

    private void ShowTitle()
    {
        gameOverUI.SetActive(false);
        player.SetActive(false);
        pipeSpawner.SetActive(false);
        scoreUI.SetActive(false);
        titleUI.SetActive(true);

        Time.timeScale = 0f;
    }

    private void BeginGame()
    {
        gameOverUI.SetActive(false);
        titleUI.SetActive(false);
        scoreUI.SetActive(true);
        player.SetActive(true);
        pipeSpawner.SetActive(true);

        ground.StartMoving();

        Time.timeScale = 1f;
    }

    public void GameOver()
    {
        gameOverUI.SetActive(true);
        player.SetActive(false);
        pipeSpawner.SetActive(false);
        scoreUI.SetActive(false);
        cardUI.SetActive(true);

        Score.SetCardScore();
        cardScoreText.text = PlayerPrefs.GetInt(HighScoreKey).ToString();
        cardHighScoreText.text = PlayerPrefs.GetInt(ScoreKey).ToString();

        if (Score.GetScore() >= ARGENT && Score.GetScore() < OR)
        {
            medallionArgent.SetActive(true);
            medallionOr.SetActive(false);
        }
        else if (Score.GetScore() >= OR)
        {
            medallionOr.SetActive(true);
            medallionArgent.SetActive(false);
        }

        ground.StopMoving();

        Time.timeScale = 0f;
    }

    public void StartGame()
    {
        _startOnLoad = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}