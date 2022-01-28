using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BattleManager
{
    private int totalPlayerCount;
    private float maxMatchTime;
    private float currentTime;
    
    private MapPhase mapPhase;
    private GameMode gameMode;

    private List<Player> playerList;
    private List<int> playerStocks;
    private List<Vector2> playerSpawnPos;

    public void SpawnMap()
    {
        return;
    }

    public void SpawnPlayers()
    {
        return;
    }

    public void TimerCountdown()
    {
        return;
    }

    public void UpdatePlayerStock(int playerNumber, int stockChange)
    {
        return;
    }

    public void UpdateDualityCount(int playerNumber, int dualityChange)
    {
        return;
    }
}
