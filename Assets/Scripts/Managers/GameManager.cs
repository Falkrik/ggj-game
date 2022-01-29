using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private BattleManager battleManager;

    [SerializeField] private int playerCount;
    [SerializeField] private int playerStockCount;
    [SerializeField] private float maxMatchTime;

    public static GameManager gameManager;
    
    public int PlayerCount { get => playerCount; }
    public int PlayerStockCount { get => playerStockCount; }
    public float MaxMatchTime { get => maxMatchTime; }

    public void Start()
    {
        InitGameManager();
    }

    private void InitGameManager()
    {
        if (battleManager == null)
            throw new NotImplementedException("BattleManager object was not added to the GameManager.");


    }

    public void RefreshBattleManager()
    {
        battleManager.InitBattleManager();
        return;
    }

    public void UpdateUserConfig()
    {
        return;
    }
}
