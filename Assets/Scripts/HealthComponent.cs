using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    private int health;

    void Awake()
    {
        health = maxHealth;
    }

    // Getter untuk nilai health
    public int Health => health;

    // Mengurangi nilai health dan menghancurkan objek jika health <= 0
    public void Subtract(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
