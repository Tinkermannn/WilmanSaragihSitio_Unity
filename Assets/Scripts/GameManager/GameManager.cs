using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public LevelManager LevelManager { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        // Cari dan assign LevelManager
        LevelManager = GetComponentInChildren<LevelManager>();

        // GameManager tidak didestroy saat pindah scene
        DontDestroyOnLoad(gameObject);

        // Cari objek Player dan Camera 
        GameObject player = GameObject.FindWithTag("Player");
        GameObject camera = GameObject.FindWithTag("MainCamera");

        if (player != null && player.transform.parent == null) // Pastikan Player bukan child dari GameManager
        {
            DontDestroyOnLoad(player); // Jangan hancurkan Player ketika pindah scene
        }

        if (camera != null && camera.transform.parent == null) // Pastikan Camera bukan child dari GameManager
        {
            DontDestroyOnLoad(camera); // Jangan hancurkan Camera ketika pindah scene
        }
    }
}
