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
        // Nonaktifkan semua spawner di awal
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
            
            // Cek apakah timer sudah habis
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
            // Pastikan enemyPrefab sudah diatur
            if (spawner.enemyPrefab != null)
            {
                int enemyLevel = spawner.enemyPrefab.level;
                
                // Aktifkan spawner jika level musuh sesuai dengan wave
                if (enemyLevel == waveNumber)
                {
                    spawner.spawnCount = spawner.defaultSpawnCount;
                    totalEnemies += spawner.spawnCount;
                    spawner.isSpawning = true;
                    Debug.Log($"Spawner {spawner.gameObject.name} aktif dengan {spawner.spawnCount} enemy level {enemyLevel}");
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
        Debug.Log($"Wave {waveNumber} selesai!");
        
        // Nonaktifkan semua spawner
        foreach (var spawner in enemySpawners)
        {
            spawner.isSpawning = false;
        }

        // Siapkan wave berikutnya
        waveNumber++;
        waveInProgress = false;
        isWaitingForNextWave = true;
        timer = waveInterval;
    }
}