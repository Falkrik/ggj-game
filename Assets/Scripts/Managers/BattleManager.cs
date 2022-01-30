using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BattleManager : MonoBehaviour
{

    [SerializeField] private GameObject mapPrefabA;
    [SerializeField] private GameObject mapPrefabB;
    [SerializeField] private GameObject playerPrefabA;
    [SerializeField] private GameObject playerPrefabB;
    [SerializeField] private List<Vector2> playerSpawnPositions;

    private GameObject mapA;
    private GameObject mapB;
    private Player winner;

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
        ClearPlayerList();
        maxMatchTime = GameManager.Instance.MaxMatchTime;
        currentTime = maxMatchTime;
        currentPlayerList = new List<Player>();
        playerStocks = new List<int>();
        playerDuality = new List<int>();

        SpawnMap();
        SpawnAllPlayers();
    }

    private void ClearPlayerList()
    {
        foreach (Player playerObject in currentPlayerList)
            Destroy(playerObject.gameObject);

        if (playerDuality == null)
            return;

        playerDuality.Clear();
        playerStocks.Clear();
        currentPlayerList.Clear();
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
        UIManager.manager.UpdateStockCount(playerNumber + 1, playerStocks[playerNumber]);

        if (playerStocks[playerNumber] < 1)
        {
            EndMatch();
            return;
        }       

        currentPlayerList[playerNumber].InitPlayer(playerSpawnPositions[playerNumber]);
    }

    /// <summary>
    /// Increments the Duality ability by the passed integer. If the integer is negative (using the move), the map will change accordingly.
    /// </summary>
    /// <param name="playerNumber">Element corresponding to player number. Player 1 is playerNumber 0, player 2 is playerNumber 1, etc</param>
    /// <param name="dualityChange">The amount to increment the total Duality count by. 1 will increase, -1 will decrease. </param>
    public void UpdateDualityCount(int playerNumber, int dualityChange)
    {
        if (playerDuality[playerNumber] < 1 && dualityChange < 1)
            return; 

        if (dualityChange < 0)
            SwapMap();

        playerDuality[playerNumber] += dualityChange;

        UIManager.manager.UpdateDuality(playerNumber + 1, playerDuality[playerNumber]);
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

    private void SpawnAllPlayers()
    {
        GameObject player0 = Instantiate(playerPrefabA, this.transform);

        currentPlayerList.Add(player0.GetComponent<Player>());
        player0.GetComponent<Player>().PlayerNumber = 0;
        currentPlayerList[0].InitPlayer(playerSpawnPositions[0]);
        playerStocks.Add(GameManager.Instance.PlayerStockCount);
        playerDuality.Add(0);

        GameObject player1 = Instantiate(playerPrefabB, this.transform);

        currentPlayerList.Add(player1.GetComponent<Player>());
        player1.GetComponent<Player>().PlayerNumber = 1;
        currentPlayerList[1].InitPlayer(playerSpawnPositions[1]);
        playerStocks.Add(GameManager.Instance.PlayerStockCount);
        playerDuality.Add(0);

        UIManager.manager.UpdateDuality(1, playerDuality[0]);
        UIManager.manager.UpdateDuality(2, playerDuality[1]);
<<<<<<< HEAD
        UIManager.manager.UpdateStockCount(1, playerStocks[0]);
        UIManager.manager.UpdateStockCount(2, playerStocks[1]);


=======
        UIManager.manager.UpdateStockCount(1, 4);
        UIManager.manager.UpdateStockCount(2, 4);
>>>>>>> origin/henrik
    }

    private void TimerCountdown()
    {
        if (!matchStarted)
            return;

        if (!matchPaused)
        {
            currentTime -= Time.deltaTime;
            UIManager.manager.UpdateTimer(currentTime);
            return;
        }

        if (currentTime <= 0f)
            EndMatch();
    }

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
        winner = currentPlayerList[0];

        foreach(Player player in currentPlayerList)
            if(playerStocks[player.PlayerNumber] >= playerStocks[winner.PlayerNumber])
                winner = player;

        UIManager.manager.WinPopup(winner.PlayerNumber + 1);

    }
}
