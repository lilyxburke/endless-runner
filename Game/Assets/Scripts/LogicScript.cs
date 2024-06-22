using System.Collections;
using System.Collections.Generic;
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
    public BrickSpawnScript spawnScript;
    public AudioSource dingSFX;

    void Start()
    {
        spawnScript = GameObject.FindGameObjectWithTag("Spawn").GetComponent<BrickSpawnScript>();
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
    }

    public void quitGame()
    {
        SceneManager.LoadScene("Start");
    }


}
