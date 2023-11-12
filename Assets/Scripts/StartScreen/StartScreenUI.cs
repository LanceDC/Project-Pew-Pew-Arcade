using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartScreenUI : MonoBehaviour
{
    public GameObject nameTextBoxGameObject;
    private string playerName = "N/A";

    private readonly int defaultTextSize = 32;
    private readonly int selectedTextSize = 54;

    private AudioSource source;

    void Start()
    {
        source = GetComponent<AudioSource>();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }


    public void SaveName()
    {
        //Save the string that was typed in the "Enter Box" box
        playerName = nameTextBoxGameObject.GetComponentsInChildren<Text>()[1].text;

        GameManager manager = gameObject.AddComponent<GameManager>();
        manager.playerName = playerName;
        manager.currentScore = 0f;

        //Increase the number of games played by 1
        GameManager.GameNumber gameNumber = SaveSystem.LoadGameNumber();

        if (gameNumber != null)
            gameNumber.gameNumber++;
        else
            gameNumber.gameNumber = 0;

        SaveSystem.SaveGameNumber(gameNumber);

        //Save the player name with the game number for the scoreboard
        SaveSystem.SavePlayer(manager, gameNumber.gameNumber);
    }

    #region Button Functions
    public void ChangeSceneButton(string sceneName)
    {
        SceneManagment.LoadScene(sceneName);
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void PlayButton()
    {
        nameTextBoxGameObject.SetActive(true);
    }

    #endregion

    #region Edit Buttons
    //Chanages the size of the text when button is selected and deselected
    public void HoverOverButton(int buttonNumber)
    {
        GetComponentsInChildren<Text>()[buttonNumber].fontSize = selectedTextSize;
        source.Play();
    }

    public void DefaultTextSize(int buttonNumber)
    {
        GetComponentsInChildren<Text>()[buttonNumber].fontSize = defaultTextSize;
    }
    #endregion
}
