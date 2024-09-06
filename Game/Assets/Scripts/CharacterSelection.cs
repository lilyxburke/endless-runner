using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterSelection : MonoBehaviour
{
    [SerializeField] private Sprite[] characters;
    [SerializeField] private int selectedCharacter = 0;
    [SerializeField] private Image playerSprite;

    void Awake()
    {
        playerSprite.sprite = characters[selectedCharacter];
    }
    public void NextCharacter()
    {
        selectedCharacter = (selectedCharacter + 1) % characters.Length;
        playerSprite.sprite = characters[selectedCharacter];
    }

    public void PreviousCharacter()
    {
        selectedCharacter--;
        if (selectedCharacter < 0)
        {
            selectedCharacter += characters.Length;
        }
        playerSprite.sprite = characters[selectedCharacter];
    }

    public void StartGame()
    {
        PlayerPrefs.SetInt("SelectedCharacter", selectedCharacter);
        SceneManager.LoadScene("Game");
    }
}
