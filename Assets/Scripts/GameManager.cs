using System;
using UnityEngine;
using UnityEngine.Audio;

public class GameManager : MonoBehaviour
{
    private const string HighScoreKey = "HighScore";
    public static GameManager Instance;


    [SerializeField] private GameObject player;
    [SerializeField] private GameObject pipeSpawner;
    [SerializeField] private LoopGround ground;

    private int Score { get; set; }

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

        AwakeGame();
    }

    public event Action<int> OnScoreChanged;
    public event Action OnStartGame;
    public event Action<int, int> OnGameOver;

    private void AwakeGame()
    {
        player.SetActive(false);
        pipeSpawner.SetActive(false);

        Time.timeScale = 0f;
    }

    public void StartGame()
    {
        ClearGame();
        OnStartGame?.Invoke();

        player.SetActive(true);
        pipeSpawner.SetActive(true);

        ground.StartMoving();

        Time.timeScale = 1f;
    }

    private void ClearGame()
    {
        Score = 0;
        OnScoreChanged?.Invoke(Score);

        player.transform.position = new Vector3(0f, 0.2f, 0f);
        player.transform.rotation = Quaternion.identity;
        var rb = player.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }

        foreach (Transform pipe in pipeSpawner.transform)
            Destroy(pipe.gameObject);
        
        player.SetActive(false);
        pipeSpawner.SetActive(false);
        ground.StopMoving();

        Time.timeScale = 0f;
    }

    public void GameOver()
    {
        var highScore = PlayerPrefs.GetInt(HighScoreKey);

        OnGameOver?.Invoke(Score, highScore);

        player.SetActive(false);
        pipeSpawner.SetActive(false);

        ground.StopMoving();

        Time.timeScale = 0f;
    }

    public void AddPoint()
    {
        Score++;

        var highScore = PlayerPrefs.GetInt(HighScoreKey, 0);
        if (Score > highScore)
            PlayerPrefs.SetInt(HighScoreKey, Score);

        OnScoreChanged?.Invoke(Score);
    }
}