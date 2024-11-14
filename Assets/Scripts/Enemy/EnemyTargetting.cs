using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTargeting : Enemy
{
    public float speed = 5f; // Speed toward the player

    void Update()
    {
        // Check if playerTransform is valid
        if (playerTransform != null)
        {
            // Calculate direction toward the player
            Vector2 direction = (playerTransform.position - transform.position).normalized;
            // Move the enemy toward the player
            transform.Translate(direction * speed * Time.deltaTime);
        }
    }

    // Collision detection with player
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject); // Destroy enemy on collision
        }
    }
}
