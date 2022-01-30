using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private BattleManager battleManager;
    [SerializeField] private ParticleSystemController particleSystemController;

    [SerializeField] private int playerStockCount;
    [SerializeField] private float maxMatchTime;

    public static GameManager Instance;
    
    public BattleManager BattleManager { get => battleManager; }
    public ParticleSystemController ParticleController { get => particleSystemController; }
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
            battleManager = GetComponent<BattleManager>();
        if (particleSystemController == null)
            particleSystemController = GetComponent<ParticleSystemController>();
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
