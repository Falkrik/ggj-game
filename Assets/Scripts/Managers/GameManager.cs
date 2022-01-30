using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private BattleManager battleManager;

    [SerializeField] private int playerStockCount;
    [SerializeField] private float maxMatchTime;

    public static GameManager Instance;
    
    public BattleManager BattleManager { get => battleManager; }
    public int PlayerStockCount { get => playerStockCount; }
    public float MaxMatchTime { get => maxMatchTime; }

    private void Awake()
    {
        if (Instance != null)
            Destroy(this);
        Instance = this;

    }
    private void Start()
    {
        InitGameManager();
        BattleManager.InitBattleManager();
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
