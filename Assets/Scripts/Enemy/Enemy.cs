using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Stats")]
    public int level = 1; // Level musuh
    private Rigidbody2D rb; // Referensi Rigidbody2D
    protected Transform playerTransform; // Referensi ke Transform pemain

    private void Awake()
    {
        // Mendapatkan komponen Rigidbody2D
        rb = GetComponent<Rigidbody2D>();
        
        // Mengatur agar Rigidbody2D tidak terpengaruh gravitasi
        rb.gravityScale = 0f;
    }

    private void Start()
    {
        // Menemukan pemain berdasarkan tag "Player"
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        // Menghadap ke pemain
        FacePlayer();
    }

    private void FacePlayer()
    {
        if (playerTransform != null)
        {
            // Menghitung arah ke pemain
            Vector2 direction = playerTransform.position - transform.position;

            // Menghitung rotasi yang diperlukan untuk menghadap pemain
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // Mengatur rotasi musuh
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
    }
}
