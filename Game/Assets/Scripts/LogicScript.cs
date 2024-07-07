using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LogicScript : MonoBehaviour
{
    public int score;
    public Text scoreText;
    public GameObject gameOverScreen;
    public int hearts = 3;
    public Text heartsText;
    public Text highScoreText;
    public int highScore;
    public BrickSpawnScript spawnScript;
    public AudioSource dingSFX;

    void Start()
    {
        spawnScript = GameObject.FindGameObjectWithTag("Spawn").GetComponent<BrickSpawnScript>();
        highScore = PlayerPrefs.GetInt("HighScore");
        highScoreText.text = "High Score: " + highScore.ToString();
    }

    public void addScore(int value)
    {
 
        dingSFX.Play();
        score += value;
        scoreText.text = score.ToString();
    }

    public void loseHearts(int value)
    {
        hearts -= value;
        heartsText.text = "Hearts: " + hearts.ToString();
    }

    public void restartGame()
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
        heartsText.enabled = false;
        changeHighScore();
    }

    public void changeHighScore()
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
