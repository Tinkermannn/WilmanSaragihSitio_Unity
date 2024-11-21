using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTargeting : Enemy
{
    public float speed = 5f; // Kecepatan enemy bergerak menuju player

    void Update()
    {
        // Memeriksa apakah playerTransform valid (player ada)
        if (playerTransform != null)
        {
            // Menghitung arah dari enemy menuju player
            Vector2 direction = (playerTransform.position - transform.position).normalized;
            // Menggerakkan enemy menuju player dengan kecepatan yang sudah ditentukan
            transform.Translate(direction * speed * Time.deltaTime);
        }
    }

    // Deteksi tabrakan dengan player
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Cek apakah objek yang terkena tabrakan adalah player
        if (collision.CompareTag("Player"))
        {
            // Hapus objek enemy ketika bertabrakan dengan player
            Destroy(gameObject);
        }
    }
}
