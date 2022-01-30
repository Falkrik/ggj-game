using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BattleManager : MonoBehaviour
{

    [SerializeField] private GameObject mapPrefabA;
    [SerializeField] private GameObject mapPrefabB;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private List<GameObject> playerList;
    [SerializeField] private List<Vector2> playerSpawnPositions;

    private GameObject mapA;
    private GameObject mapB;

    private int playerCount;
    private float maxMatchTime;
    private float currentTime;
    private bool matchStarted;
    private bool matchPaused;
    
    private MapPhase mapPhase;
    private GameMode gameMode;

    private List<Player> currentPlayerList = new List<Player>();
    private List<int> playerStocks;
    private List<int> playerDuality;

    public bool MatchStarted { get => matchStarted; set => matchStarted = value; }
    public bool MatchPaused { get => matchPaused; set => matchPaused = value; }

    /// <summary>
    /// Spawns a new map and spawns new players based on the amout passed by the GameManager.
    /// </summary>
    public void InitBattleManager()
    {
        playerCount = GameManager.Instance.PlayerCount;
        maxMatchTime = GameManager.Instance.MaxMatchTime;
        currentTime = maxMatchTime;
        currentPlayerList = new List<Player>();

        SpawnMap();
        SpawnPlayers();
    }

    private void Update()
    {
        TimerCountdown();
    }

    /// <summary>
    /// Increments the number of player lives by the passed integer. If the player has remaining stocks, they will respawn. If they have no remaining
    /// stocks, the match will end.
    /// </summary>
    /// <param name="playerNumber">Element corresponding to player number. Player 1 is playerNumber 0, player 2 is playerNumber 1, etc.</param>
    /// <param name="stockChange">The amount to increment the total player lives by. 1 will increase, -1 will decrease.</param>
    public void UpdatePlayerStock(int playerNumber, int stockChange)
    {
        playerStocks[playerNumber] += stockChange;

        if (playerStocks[playerNumber] < 1)
            EndMatch();
        return;
    }

    /// <summary>
    /// Increments the Duality ability by the passed integer. If the integer is negative (using the move), the map will change accordingly.
    /// </summary>
    /// <param name="playerNumber">Element corresponding to player number. Player 1 is playerNumber 0, player 2 is playerNumber 1, etc</param>
    /// <param name="dualityChange">The amount to increment the total Duality count by. 1 will increase, -1 will decrease. </param>
    public void UpdateDualityCount(int playerNumber, int dualityChange)
    {
        if (dualityChange < 1)
            SwapMap();

        playerDuality[playerNumber] += dualityChange;
        return;
    }

    // Checks for and destroys the old map, instantiates a new one.
    private void SpawnMap()
    {
        if (mapA != null)
            Destroy(mapA);
        if (mapB != null)
            Destroy(mapB);

        mapA = Instantiate(mapPrefabA, this.transform);
        mapB = Instantiate(mapPrefabB, this.transform);

        mapB.gameObject.SetActive(false);
        mapPhase = MapPhase.A;
    }

    private void SpawnPlayers()
    {
        for(int i = 0; i < playerCount; i++)
        {
            GameObject newPlayer = Instantiate(playerPrefab, this.transform);

            currentPlayerList.Add(newPlayer.GetComponent<Player>());
            currentPlayerList[i].SpawnCharacter(playerSpawnPositions[i]);
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

    [ContextMenu("Swap Maps")]
    private void SwapMap()
    {
        Debug.Log(mapPhase);
       if(mapPhase == MapPhase.A)
       {

            mapA.gameObject.SetActive(false);
            mapB.gameObject.SetActive(true);

            mapPhase = MapPhase.B;
            return;
       }

        if (mapPhase == MapPhase.B)
        {
            mapA.gameObject.SetActive(true);
            mapB.gameObject.SetActive(false);

            mapPhase = MapPhase.A;
            return;
        }
        Debug.Log(mapPhase);

    }

    private void EndMatch()
    {
        //Here we need to build the player win/loss condition and add the UI representation.
    }
}
