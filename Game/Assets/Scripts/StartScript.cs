using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScript : MonoBehaviour
{
    public void playGame()
    {
        SceneManager.LoadScene("PlayerChoice");
    }
    public void quitGame()
    {
        Application.Quit();
    }


}
