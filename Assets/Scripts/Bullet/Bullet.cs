using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour
{
    [Header("Bullet Stats")]
    public float bulletSpeed = 20f;
    public int damage = 10;

    private Rigidbody2D rb;
    private IObjectPool<Bullet> pool;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void SetPool(IObjectPool<Bullet> bulletPool)
    {
        pool = bulletPool;
    }

    public void Launch()
    {
        // Set the bullet's initial velocity to move upward (adjust based on shooting direction)
        rb.velocity = transform.up * bulletSpeed;
    }

    // private void OnTriggerEnter2D(Collider2D collision)
    // {
    //     // Check if the collided object has a HitboxComponent
    //     HitboxComponent hitbox = collision.GetComponent<HitboxComponent>();
    //     if (hitbox != null)
    //     {
    //         // Apply damage to the HitboxComponent
    //         hitbox.Damage(damage);

    //         // Release the bullet back to the pool after collision
    //         pool?.Release(this);
    //     }
    // }

    private void OnBecameInvisible()
    {
        // Return the bullet to the pool when it goes off-screen
        pool?.Release(this);
    }
}
