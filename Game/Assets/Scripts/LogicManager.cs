using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LogicManager : MonoBehaviour
{

    [SerializeField] public int score;
    [SerializeField] private Text scoreText;
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private Text healthText;
    [SerializeField] private Text highScoreText;
    [SerializeField] private int highScore;
    [SerializeField] private BrickSpawnScript spawnScript;
    [SerializeField] private AudioSource dingSFX;

    public static LogicManager instance { get; private set; }
    public static LogicManager Instance
    {
        get
        {
            return instance;
        }
    }
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        spawnScript = GameObject.FindGameObjectWithTag("Spawn").GetComponent<BrickSpawnScript>();
        highScore = PlayerPrefs.GetInt("HighScore");
    }

    void Update()
    {
        healthText.text = $"Hearts: {PlayerScript.Instance.health}";
        scoreText.text = $"{score}";
        highScoreText.text = $"HI {highScore}";
    }
    public void addScore(int value)
    {
        dingSFX.Play();
        score += value;
        spawnScript.IncreaseSpeed();
    }

    private void restartGame()
    {
        Physics2D.IgnoreLayerCollision(8, 9, false);
        spawnScript.stopSpawn = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void gameOver()
    {
        gameOverScreen.SetActive(true);
        spawnScript.stopSpawn = true;
        scoreText.enabled = false;
        healthText.enabled = false;
        highScoreText.enabled = false;
        changeHighScore();
        GameObject[] blocks = GameObject.FindGameObjectsWithTag("Block");
        foreach (GameObject block in blocks)
        {
            Destroy(block);
        }
    }

    private void changeHighScore()
    {
        if (score > highScore)
        {
            PlayerPrefs.SetInt("HighScore", score);
        }
    }

    public void quitGame()
    {
        SceneManager.LoadScene("Start");
    }


}