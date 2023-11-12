using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class PlayerScore
{
    public int place;
    public Text placeText;
    public string playerName;
    public Text playerNameText;
    public float playerTime;
    public Text playerTimeText;

    public void UpdateUI()
    {
        placeText.text = place.ToString();

        playerNameText.text = playerName;

        playerTimeText.text = playerTime.ToString();
    }

    public void GetScoreSpaceUI(ScoreSpace space)
    {
        placeText = space.placeText;
        playerNameText = space.nameText;
        playerTimeText = space.timeText;
    }
}


[System.Serializable]
public class ScoreSpace
{
    public Text placeText;
    public Text nameText;
    public Text timeText;
}

public class PlayerScoreTable : MonoBehaviour
{
    public ScoreSpace[] scoreSpaces;


    public List<PlayerScore> playerScores;

    void Start()
    {
        //gets the number of previous games played and completed
        GameManager.GameNumber number = SaveSystem.LoadGameNumber();

        //gets the player name and their time from previous games
        for (int i = 0; i < number.gameNumber; i++)
        {

            PlayerData data = SaveSystem.LoadPlayer(i+1);

            PlayerScore score = new PlayerScore
            {
                playerName = data.playerName,
                playerTime = data.playerScore
            };

            playerScores.Add(score);
        }


        // sorts the times from lowest to highest
        playerScores.Sort(delegate (PlayerScore p1, PlayerScore p2)
        {
            return p1.playerTime.CompareTo(p2.playerTime);
        });
        
        //displays the 5 quickest times
        for (int i = 0; i < scoreSpaces.Length; i++)
        {
            if (i == playerScores.Count)
                break;

            playerScores[i].place = i + 1;
            playerScores[i].GetScoreSpaceUI(scoreSpaces[i]);
            playerScores[i].UpdateUI();
        }

       

    }
}
