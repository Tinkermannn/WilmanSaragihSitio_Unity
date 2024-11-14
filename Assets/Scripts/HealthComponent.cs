using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    private int health;

    // Initialize health with maxHealth
    void Awake()
    {
        health = maxHealth;
    }

    // Getter for current health
    public int Health => health;

    // Method to reduce health
    public void Subtract(int damage)
    {
        health -= damage;

        // Destroy the object if health reaches zero or below
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
