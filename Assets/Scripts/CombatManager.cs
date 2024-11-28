using System.Collections;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    [Header("Enemy Spawners")]
    public EnemySpawner[] enemySpawners;

    [Header("Wave Settings")]
    public float timer = 0f;
    [SerializeField] private float waveInterval = 5f;
    public int waveNumber = 1;
    public int totalEnemies = 0;

    private bool isWaitingForNextWave = true;
    private bool waveInProgress = false;

    private void Start()
    {
        // Disable all spawners at the start
        foreach (var spawner in enemySpawners)
        {
            spawner.isSpawning = false;
        }
        timer = waveInterval;
    }

    private void Update()
    {
        if (isWaitingForNextWave)
        {
            // Update timer
            timer -= Time.deltaTime;

            // Check if the timer has expired
            if (timer <= 0)
            {
                isWaitingForNextWave = false;
                timer = waveInterval;
                StartWave();
            }
        }
        else if (waveInProgress)
        {
            UpdateEnemyCount();
        }
    }

    private void StartWave()
    {
        waveInProgress = true;
        totalEnemies = 0;

        foreach (var spawner in enemySpawners)
        {
            // Ensure enemyPrefab is set
            if (spawner.enemyPrefab != null)
            {
                int enemyLevel = spawner.enemyPrefab.level;

                // Activate spawner if enemy level matches the wave
                if (enemyLevel == waveNumber)
                {
                    spawner.spawnCount = spawner.defaultSpawnCount;
                    totalEnemies += spawner.spawnCount;
                    spawner.isSpawning = true;
                    Debug.Log($"Spawner {spawner.gameObject.name} active with {spawner.spawnCount} enemies of level {enemyLevel}");
                }
                else
                {
                    spawner.spawnCount = 0;
                    spawner.isSpawning = false;
                }
            }
        }

        if (totalEnemies == 0)
        {
            EndWave();
        }
    }

    private void UpdateEnemyCount()
    {
        totalEnemies = 0;

        foreach (var spawner in enemySpawners)
        {
            if (spawner.isSpawning)
            {
                totalEnemies += spawner.spawnCount;
            }
        }

        if (totalEnemies <= 0)
        {
            EndWave();
        }
    }

    private void EndWave()
    {
        Debug.Log($"Wave {waveNumber} complete!");

        // Disable all spawners
        foreach (var spawner in enemySpawners)
        {
            spawner.isSpawning = false;
        }

        // Prepare for the next wave
        waveNumber++;
        waveInProgress = false;
        isWaitingForNextWave = true;
        timer = waveInterval;
    }

    // New method to get remaining enemies (based on active spawners)
    public int GetRemainingEnemies()
    {
        int remainingEnemies = 0;

        foreach (var spawner in enemySpawners)
        {
            if (spawner.isSpawning)
            {
                remainingEnemies += spawner.spawnCount;
            }
        }

        return remainingEnemies;
    }
}
