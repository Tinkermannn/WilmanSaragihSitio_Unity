using UnityEngine;

[RequireComponent(typeof(Collider2D))] // Ensure the object has a Collider2D
public class AttackComponent : MonoBehaviour
{
    [Header("Attack Stats")]
    public Bullet bulletPrefab;  // Reference to the Bullet prefab
    public int damage = 10;      // Damage amount
    public Transform attackOrigin;  // Position where the attack originates

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Ignore collision if tags match
        if (other.CompareTag(gameObject.tag))
        {
            return;
        }

        // Check if the other object has a HitboxComponent
        HitboxComponent hitbox = other.GetComponent<HitboxComponent>();
        if (hitbox != null)
        {
            // Apply damage to the HitboxComponent
            hitbox.Damage(damage);
        }
    }

    // Method to perform an attack, for example, shooting a bullet
    public void PerformAttack()
    {
        if (bulletPrefab != null && attackOrigin != null)
        {
            Bullet bullet = Instantiate(bulletPrefab, attackOrigin.position, attackOrigin.rotation);
            bullet.damage = damage; // Set bullet damage
            bullet.Launch();        // Launch the bullet
        }
    }
}
