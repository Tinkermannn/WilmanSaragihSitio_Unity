using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class HitboxComponent : MonoBehaviour
{
    // Referensi ke komponen HealthComponent
    private HealthComponent health;

    private void Awake()
    {
        // Mendapatkan komponen HealthComponent dari objek ini
        health = GetComponent<HealthComponent>();
        if (health == null)
        {
            Debug.LogError("HealthComponent tidak ditemukan pada objek ini.");
        }
    }

    // Method untuk menerima damage dari Bullet
    public void Damage(Bullet bullet)
    {
        if (health != null)
        {
            int damageAmount = bullet.GetDamage();
            health.Subtract(damageAmount); // Mengurangi health berdasarkan damage bullet
        }
    }

    // Method untuk menerima damage dari nilai integer
    public void Damage(int damageAmount)
    {
        if (health != null)
        {
            health.Subtract(damageAmount); // Mengurangi health berdasarkan nilai damage yang diberikan
        }
    }
}
