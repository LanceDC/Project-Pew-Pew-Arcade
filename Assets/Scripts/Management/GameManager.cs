using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public float currentScore = 0f;
    public Text scoreText;
    public Text timeText;
    public string playerName = "N/A";

    [HideInInspector]
    public float currentTime = 0f;
    [HideInInspector]
    public int gameNumber;
    

    [System.Serializable]
    public class GameNumber
    {
        public int gameNumber;

        public GameNumber()
        {
            gameNumber = 0;
        }

        public GameNumber(GameManager manager)
        {
            gameNumber = manager.gameNumber;
        }
    }

    private GameNumber number = new GameNumber();
    private float startTime;

    void Start()
    {
        //If there is no game saved then this must be the first game played
        number = SaveSystem.LoadGameNumber();
        if (number != null)
            gameNumber = number.gameNumber;
        else
            gameNumber = 0;

        PlayerData newPlayer = SaveSystem.LoadPlayer(number.gameNumber);
        playerName = newPlayer.playerName;

        //Get the current time since the app has been running
        startTime = Time.time;

    }

    void Update()
    {
        if (scoreText != null && timeText != null)
            ScoreAndTimeIncrease();
    }

    public void AddToScore(float addScore)
    {
        currentScore += addScore;
    }

    public void SaveGame()
    {
        SaveSystem.SavePlayer(this, number.gameNumber);
    }

    private void ScoreAndTimeIncrease()
    {
        //Take away the time the app has been running from the current time to get the actual game time
        currentTime = Time.time - startTime;

        /*
        currentTime *= 100f;
        currentTime = Mathf.Round(currentTime);
        currentTime /= 100f;
        timeText.text = "Time : " + currentTime.ToString();
        */

        //Changes time from having loads of decimal points to just two
        timeText.text = "Time : " + currentTime.ToString("F");

    }


    //Deletes all scores that have been saved
    public void ClearPlayerData()
    {
        List<PlayerScore> playerScores = new List<PlayerScore>();
        gameNumber = 5;

        for (int i = 0; i < gameNumber; i++)
        {
            PlayerData data = SaveSystem.LoadPlayer(i + 1);
            PlayerScore score = new PlayerScore
            {
                playerName = data.playerName,
                playerTime = data.playerScore
            };

            playerScores.Add(score);
        }
        
        for (int i = 0; i < playerScores.Count; i++)
            SaveSystem.SavePlayer(this, 0);

        SaveSystem.SaveGameNumber(new GameNumber());
    }
}
