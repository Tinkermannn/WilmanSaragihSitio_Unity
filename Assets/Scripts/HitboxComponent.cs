using UnityEngine;

[RequireComponent(typeof(Collider2D))] // Ensure the object has a Collider2D
public class HitboxComponent : MonoBehaviour
{
    private HealthComponent health;

    void Awake()
    {
        // Get the HealthComponent on the same object
        health = GetComponent<HealthComponent>();
        if (health == null)
        {
            Debug.LogError("HealthComponent not found on the object.");
        }
    }

    // Damage method accepting Bullet object
    public void Damage(Bullet bullet)
    {
        if (health != null)
        {
            health.Subtract(bullet.damage);
        }
    }

    // Damage method accepting int value
    public void Damage(int damage)
    {
        if (health != null)
        {
            health.Subtract(damage);
        }
    }
}
