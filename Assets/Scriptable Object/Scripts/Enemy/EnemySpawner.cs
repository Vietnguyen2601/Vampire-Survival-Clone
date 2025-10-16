using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [System.Serializable]
    public class Wawe
    {
        public string waweName;
        public List<EnemyGroup> enemyGroups;
        public int waweQuota; // the total of enemies to spawn in this wave
        public float spawnInterval; // the interval at which enemies spawn
        public int spawnedCount; // the number of enemies spawned so far
    }

    [System.Serializable]
    public class EnemyGroup
    {
        public string enemyName;
        public int enemyCount;
        public int spawnCount;
        public GameObject enemyPrefab;

    }

    public List<Wawe> wawes;
    public int currentWaweCount;

    [Header("Spawn Timer")]
    float spawnTimer;
    public int enemiesAlive;
    public int maxEnemiesAllowed;
    public bool maxEnemiesReached = false;
    public float waweInterval; // the interval between waves

    [Header("Spawn Position")]
    public List<Transform> relativeSpawnPoints;


    Transform player;

    void Start()
    {
        player = FindObjectOfType<PlayerStats>().transform;
        CaculateWaweQuota();
    }

    void Update()
    {
        if (currentWaweCount < wawes.Count && wawes[currentWaweCount].spawnedCount == 0)
        {
            StartCoroutine(BeginNextWawe());
        }

        spawnTimer += Time.deltaTime;

        if (spawnTimer >= wawes[currentWaweCount].spawnInterval)
        {
            spawnTimer = 0f;
            SpawnEnemies();
        }
    }

    IEnumerator BeginNextWawe()
    {
        yield return new WaitForSeconds(waweInterval);

        if (currentWaweCount < wawes.Count - 1)
        {
            currentWaweCount++;
            CaculateWaweQuota();
        }
    }

    void CaculateWaweQuota()
    {
        int currentWaweQuota = 0;
        foreach (var enemyGroup in wawes[currentWaweCount].enemyGroups)
        {
            currentWaweQuota += enemyGroup.enemyCount;
        }

        wawes[currentWaweCount].waweQuota = currentWaweQuota;
        Debug.LogWarning("Current Wawe Quota: " + currentWaweQuota);
    }

    void SpawnEnemies()
    {
        if (wawes[currentWaweCount].spawnedCount < wawes[currentWaweCount].waweQuota && !maxEnemiesReached)
        {
            foreach(var enemyGroup in wawes[currentWaweCount].enemyGroups)
            {
                if (enemyGroup.spawnCount < enemyGroup.enemyCount)
                {
                    if(enemiesAlive > maxEnemiesAllowed)
                    {
                        maxEnemiesReached = true;
                        return;
                    }

                    Instantiate(enemyGroup.enemyPrefab, player.position + relativeSpawnPoints[UnityEngine.Random.Range(0, relativeSpawnPoints.Count)].position, Quaternion.identity); 

                    //Vector2 spawnPosition = new Vector2(player.transform.position.x + UnityEngine.Random.Range(-10f, 10f), player.transform.position.y + UnityEngine.Random.Range(-10f, 10f));
                    //Instantiate(enemyGroup.enemyPrefab, spawnPosition, Quaternion.identity);

                    enemyGroup.spawnCount++;
                    wawes[currentWaweCount].spawnedCount++;
                    enemiesAlive++;
                }
            }
        }

        if(enemiesAlive < maxEnemiesAllowed)
        {
            maxEnemiesReached = false;
        }

    }

    public void OnEnemyKilled()
    {
        enemiesAlive--;
    }
}
