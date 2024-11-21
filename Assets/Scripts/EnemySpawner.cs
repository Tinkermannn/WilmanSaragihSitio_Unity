using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Prefabs")]
    public Enemy spawnedEnemy; // Prefab enemy yang akan di-spawn

    [SerializeField] private int minimumKillsToIncreaseSpawnCount = 3;
    public int totalKill = 0; // Total kill global
    private int totalKillWave = 0; // Total kill dalam satu wave

    [SerializeField] private float spawnInterval = 3f;

    [Header("Spawned Enemies Counter")]
    public int spawnCount = 0; // Jumlah yang di-spawn saat ini
    public int defaultSpawnCount = 1; // Default jumlah spawn
    public int spawnCountMultiplier = 1; // Multiplikasi spawn
    public int multiplierIncreaseCount = 1; // Berapa kali multiplier meningkat

    public CombatManager combatManager; // Referensi ke manajer combat

    public bool isSpawning = false;

    private void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    private void Update()
    {
        CheckKillProgress();
    }

    // Coroutine untuk melakukan spawn secara berkala
    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            if (isSpawning)
            {
                for (int i = 0; i < spawnCount; i++)
                {
                    SpawnEnemy();
                    yield return new WaitForSeconds(spawnInterval); // Jeda antar spawn
                }
            }

            yield return null;
        }
    }

    // Fungsi untuk memeriksa progres kill dan menyesuaikan jumlah spawn
    private void CheckKillProgress()
    {
        if (totalKillWave >= minimumKillsToIncreaseSpawnCount)
        {
            spawnCountMultiplier += multiplierIncreaseCount;
            spawnCount = defaultSpawnCount * spawnCountMultiplier;
            totalKillWave = 0; // Reset kill wave setelah spawn count bertambah
        }
    }

    // Fungsi untuk melakukan spawn enemy
    private void SpawnEnemy()
    {
        if (spawnedEnemy != null)
        {
            Instantiate(spawnedEnemy, transform.position, Quaternion.identity);
            spawnCount--; // Kurangi jumlah yang perlu di-spawn di wave ini
        }
    }

    // Fungsi ini dipanggil oleh CombatManager saat enemy terbunuh
    public void OnEnemyKilled()
    {
        totalKill++;
        totalKillWave++;
    }
}
