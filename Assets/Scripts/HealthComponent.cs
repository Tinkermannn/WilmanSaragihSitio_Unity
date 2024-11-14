using UnityEngine;

// Komponen yang bisa ditambahkan ke setiap GameObject yang membutuhkan health
public class HealthComponent : MonoBehaviour
{
    [SerializeField] private int maxHealth; // Batas maksimum health
    private int health;

    // Properti read-only untuk mendapatkan nilai health saat ini
    public int Health => health;

    // Initialize the health value to maxHealth when the object is created
    private void Start()
    {
        health = maxHealth; // Inisialisasi health ke maxHealth
    }

    // Method untuk mengurangi health
    public void Subtract(int damage)
    {
        health -= damage; // Kurangi nilai health
        if (health <= 0)
        {
            Destroy(gameObject); // Hapus objek jika health <= 0
        }
    }
}
