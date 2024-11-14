using UnityEngine;

[RequireComponent(typeof(Collider2D), typeof(HealthComponent))]
public class HitboxComponent : MonoBehaviour
{
    private HealthComponent health;

    void Awake()
    {
        health = GetComponent<HealthComponent>();
    }

    // Method overload untuk menerima damage dari objek Bullet
    public void Damage(Bullet bullet)
    {
        if (health != null)
        {
            health.Subtract(bullet.damage);
        }
    }

    // Method overload untuk menerima damage dalam bentuk integer
    public void Damage(int damage)
    {
        if (health != null)
        {
            health.Subtract(damage);
        }
    }
}
