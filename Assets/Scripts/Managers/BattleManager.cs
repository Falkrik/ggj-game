using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BattleManager : MonoBehaviour
{

    [SerializeField] private GameObject mapPrefab;
    [SerializeField] private List<Player> playerPrefabList;
    [SerializeField] private List<Vector2> playerSpawnPositions;

    private GameObject map;

    private int playerCount;
    private float maxMatchTime;
    private float currentTime;
    private bool matchStarted;
    private bool matchPaused;
    
    private MapPhase mapPhase;
    private GameMode gameMode;

    private List<Player> currentPlayerList;
    private List<int> playerStocks;
    private List<int> playerDuality;
    private List<Vector2> playerSpawnPos;

    public bool MatchStarted { get => matchStarted; set => matchStarted = value; }
    public bool MatchPaused { get => matchPaused; set => matchPaused = value; }

    /// <summary>
    /// Spawns a new map and spawns new players based on the amout passed by the GameManager.
    /// </summary>
    public void InitBattleManager()
    {
        playerCount = GameManager.gameManager.PlayerCount;
        maxMatchTime = GameManager.gameManager.MaxMatchTime;
        
        SpawnMap();
        SpawnPlayers();
    }

    /// <summary>
    /// Increments the number of player lives by the passed integer.
    /// </summary>
    /// <param name="playerNumber">Element corresponding to player number. Player 1 is playerNumber 0, player 2 is playerNumber 1, etc.</param>
    /// <param name="stockChange">The amount to increment the total player lives by. 1 will increase, -1 will decrease.</param>
    public void UpdatePlayerStock(int playerNumber, int stockChange)
    {
        playerStocks[playerNumber] += stockChange;
        return;
    }

    /// <summary>
    /// Increments the Duality ability by the passed integer.
    /// </summary>
    /// <param name="playerNumber">Element corresponding to player number. Player 1 is playerNumber 0, player 2 is playerNumber 1, etc</param>
    /// <param name="dualityChange">The amount to increment the total Duality count by. 1 will increase, -1 will decrease. </param>
    public void UpdateDualityCount(int playerNumber, int dualityChange)
    {
        playerDuality[playerNumber] += dualityChange;
        return;
    }

    private void Update()
    {
        TimerCountdown();
    }

    // Checks for and destroys the old map, instantiates a new one.
    private void SpawnMap()
    {
        if (map != null)
            Destroy(map);

        map = Instantiate(mapPrefab, this.transform);
    }

    private void SpawnPlayers()
    {
        currentPlayerList = new List<Player>();

        for(int i = 0; i < playerCount; i++)
        {
            currentPlayerList.Add(playerPrefabList[i]);
            currentPlayerList[i].SpawnCharacter();
        }
    }

    private void TimerCountdown()
    {
        if (!matchStarted)
            return;

        if (!matchPaused)
        {
            currentTime -= Time.deltaTime;
            return;
        }

        if (currentTime <= 0f)
            EndMatch();
    }

    private void EndMatch()
    {
        //Here we need to build the player win/loss condition and add the UI representation.
    }
}
