using System.Collections;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    [Header("Enemy Spawners")]
    public EnemySpawner[] enemySpawners; // Daftar semua spawner yang aktif

    [Header("Wave Settings")]
    public float timer = 0f; // Timer untuk wave interval
    [SerializeField] private float waveInterval = 5f; // Interval antar wave
    public int waveNumber = 1; // Nomor wave saat ini
    public int totalEnemies = 0; // Total musuh di wave saat ini

    private bool waveInProgress = false; // Status apakah wave sedang berjalan

    private void Start()
    {
        StartCoroutine(ManageWaves());
    }

    private void Update()
    {
        if (waveInProgress)
        {
            UpdateEnemyCount();
        }
    }

    // Coroutine untuk mengatur siklus wave
    private IEnumerator ManageWaves()
    {
        while (true)
        {
            // Tunggu waktu wave interval sebelum memulai wave berikutnya
            yield return new WaitForSeconds(waveInterval);

            // Memulai wave baru
            StartWave();

            // Tunggu hingga wave selesai
            while (waveInProgress)
            {
                yield return null;
            }
        }
    }

    // Memulai wave baru
    private void StartWave()
    {
        Debug.Log($"Wave {waveNumber} dimulai!");

        waveInProgress = true;
        totalEnemies = 0;

        // Atur jumlah enemy berdasarkan spawner
        foreach (var spawner in enemySpawners)
        {
            spawner.spawnCount = spawner.defaultSpawnCount * waveNumber; // Tingkatkan spawn berdasarkan wave
            totalEnemies += spawner.spawnCount; // Hitung total musuh di wave ini
            spawner.isSpawning = true; // Aktifkan spawner
        }
    }

    // Perbarui jumlah enemy yang tersisa di wave ini
    private void UpdateEnemyCount()
    {
        totalEnemies = 0;

        // Hitung total musuh aktif dari semua spawner
        foreach (var spawner in enemySpawners)
        {
            totalEnemies += spawner.spawnCount;
        }

        // Jika tidak ada musuh tersisa, akhiri wave
        if (totalEnemies <= 0)
        {
            EndWave();
        }
    }

    // Akhiri wave saat ini dan persiapkan wave berikutnya
    private void EndWave()
    {
        Debug.Log($"Wave {waveNumber} selesai!");
        waveNumber++; // Naikkan nomor wave
        waveInProgress = false; // Tandai bahwa wave selesai

        // Nonaktifkan spawner hingga wave berikutnya
        foreach (var spawner in enemySpawners)
        {
            spawner.isSpawning = false;
        }
    }
}
