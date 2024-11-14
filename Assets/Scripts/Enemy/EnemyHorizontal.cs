using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHorizontal : Enemy
{
    public float speed = 5f; // Kecepatan bergerak
    private Vector2 direction;

    void Start()
    {
        // Menentukan lebar layar dalam world units
        float screenWidth = Camera.main.orthographicSize * Camera.main.aspect;
        Vector2 spawnPosition;

        if (Random.value < 0.5f)
        {
            // Spawn dari sisi kiri layar
            spawnPosition = new Vector2(-screenWidth - 1, Random.Range(-Camera.main.orthographicSize, Camera.main.orthographicSize));
            direction = Vector2.right; // Gerak ke kanan
        }
        else
        {
            // Spawn dari sisi kanan layar
            spawnPosition = new Vector2(screenWidth + 1, Random.Range(-Camera.main.orthographicSize, Camera.main.orthographicSize));
            direction = Vector2.left; // Gerak ke kiri
        }

        transform.position = spawnPosition;
    }

    void Update()
    {
        // Pindahkan EnemyHorizontal sesuai arah
        transform.Translate(direction * speed * Time.deltaTime);

        // Cek apakah objek sudah keluar dari layar
        float screenWidth = Camera.main.orthographicSize * Camera.main.aspect;
        if (Mathf.Abs(transform.position.x) > screenWidth + 1)
        {
            // Ubah arah dan posisi Y secara acak
            direction = -direction;
            float randomY = Random.Range(-Camera.main.orthographicSize, Camera.main.orthographicSize);
            transform.position = new Vector2(transform.position.x, randomY);
        }
    }
}
