using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Prefabs")]
    public Enemy enemyPrefab;

    [SerializeField] private int minimumKillsToIncreaseSpawnCount = 3;
    public int totalKill = 0;
    private int totalKillWave = 0;

    [SerializeField] private float spawnInterval = 3f;

    [Header("Spawned Enemies Counter")]
    public int spawnCount = 0;
    public int defaultSpawnCount = 1;
    public int spawnCountMultiplier = 1;
    public int multiplierIncreaseCount = 1;

    public CombatManager combatManager;

    public bool isSpawning = false;

    private void Start()
    {
        if (combatManager == null)
        {
            combatManager = FindObjectOfType<CombatManager>();
            if (combatManager == null)
            {
                Debug.LogError("CombatManager tidak ditemukan! Pastikan ada di scene.");
            }
        }

        StartCoroutine(SpawnEnemies());
    }

    private void Update()
    {
        CheckKillProgress();
    }

    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            if (isSpawning)
            {
                for (int i = 0; i < spawnCount; i++)
                {
                    SpawnEnemy();
                    yield return new WaitForSeconds(spawnInterval);
                }
                isSpawning = false; // Hentikan spawning setelah selesai
            }

            yield return null;
        }
    }

    private void CheckKillProgress()
    {
        if (totalKillWave >= minimumKillsToIncreaseSpawnCount)
        {
            spawnCountMultiplier += multiplierIncreaseCount;
            spawnCount = defaultSpawnCount * spawnCountMultiplier;
            totalKillWave = 0;
        }
    }

    private void SpawnEnemy()
    {
        if (enemyPrefab != null)
        {
            Enemy enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            enemy.SetLevel(combatManager.waveNumber); // Set level sesuai wave
            spawnCount--;
        }
        else
        {
            Debug.LogError("Enemy prefab tidak diatur di EnemySpawner!");
        }
    }

    public void OnEnemyKilled()
    {
        totalKill++;
        totalKillWave++;
    }
}
