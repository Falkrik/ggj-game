using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DualitySpawner : MonoBehaviour
{
    [SerializeField] private GameObject dualityObject;
    [SerializeField] private List<Vector2> dualitySpawns;
    [SerializeField] private float dualitySpawnTime;
    private float currentSpawnTime = 0f;

    private void Update()
    {
        SpawnTimeCheck();
    }

    private void SpawnTimeCheck()
    {
        currentSpawnTime += Time.deltaTime;

        if (currentSpawnTime >= dualitySpawnTime)
        {
            SpawnDuality();
            currentSpawnTime = 0;
        }
    }

    private void SpawnDuality()
    {
        int randomLoc = UnityEngine.Random.Range(0, dualitySpawns.Count);
        Vector2 spawnLoc = dualitySpawns[randomLoc];

        GameObject dualitySpawn = Instantiate(dualityObject, transform);
        dualitySpawn.transform.position = spawnLoc;
        dualitySpawn.SetActive(true);
    }
}
