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

    public void addScore(int value)
    {
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void gameOver()
    {
        gameOverScreen.SetActive(true);
    }
}
