using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyForward : Enemy
{
    public float speed = 5f; // Kecepatan gerak
    private Vector2 direction;

    void Start()
    {
        // Tentukan tinggi layar dalam world units
        float screenHeight = Camera.main.orthographicSize;
        Vector2 spawnPosition;

        if (Random.value < 0.5f)
        {
            // Spawn dari atas layar (sedikit di luar layar)
            spawnPosition = new Vector2(Random.Range(-Camera.main.orthographicSize * Camera.main.aspect, Camera.main.orthographicSize * Camera.main.aspect), screenHeight + 1);
            direction = Vector2.down; // Gerak ke bawah
        }
        else
        {
            // Spawn dari bawah layar (sedikit di luar layar)
            spawnPosition = new Vector2(Random.Range(-Camera.main.orthographicSize * Camera.main.aspect, Camera.main.orthographicSize * Camera.main.aspect), -screenHeight - 1);
            direction = Vector2.up; // Gerak ke atas
        }

        transform.position = spawnPosition;
    }

    void Update()
    {
        // Pindahkan EnemyForward sesuai arah
        transform.Translate(direction * speed * Time.deltaTime);

        // Cek apakah objek sudah keluar dari batas atas atau bawah
        float screenHeight = Camera.main.orthographicSize;
        if (Mathf.Abs(transform.position.y) > screenHeight + 1)
        {
            // Ubah arah dan posisi X secara acak
            direction = -direction;
            float randomX = Random.Range(-Camera.main.orthographicSize * Camera.main.aspect, Camera.main.orthographicSize * Camera.main.aspect);
            transform.position = new Vector2(randomX, transform.position.y);
        }
    }
}
