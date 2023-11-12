using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public string playerName;
    public float playerScore;

    public PlayerData(GameManager manager)
    {
        playerName = manager.playerName;
        playerScore = manager.currentTime;
    }
}
