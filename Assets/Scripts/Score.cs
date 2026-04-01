using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    private const string HighScoreKey = "HighScore";
    private const string ScoreKey = "Score";
    public static Score Instance;

    private static int _score;

    [SerializeField] private TextMeshProUGUI scoreText;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        _score = 0;
        scoreText.text = _score.ToString();
        UpdateHighScore();
    }

    private void UpdateHighScore()
    {
        if (!PlayerPrefs.HasKey(HighScoreKey)) PlayerPrefs.SetInt(HighScoreKey, 0);
        if (_score > PlayerPrefs.GetInt(HighScoreKey))
        {
            PlayerPrefs.SetInt(HighScoreKey, _score);
            PlayerPrefs.Save();
        }
    }

    public void UpdateScore()
    {
        _score++;
        scoreText.text = _score.ToString();
        UpdateHighScore();
    }

    public static int GetScore()
    {
        return _score;
    }

    public static void SetCardScore()
    {
        PlayerPrefs.SetInt(ScoreKey, _score);
        PlayerPrefs.SetInt(HighScoreKey, PlayerPrefs.GetInt(HighScoreKey, _score));
        PlayerPrefs.Save();
    }
}