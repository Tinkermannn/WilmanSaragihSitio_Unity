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

    // Set the pool reference
    public void SetPool(IObjectPool<Bullet> bulletPool)
    {
        pool = bulletPool;
    }

    // Launch the bullet with specified speed
    public void Launch()
    {
        rb.velocity = transform.up * bulletSpeed; // Adjust direction as needed
    }

    // Return bullet to the pool when it goes off-screen
    private void OnBecameInvisible()
    {
        pool?.Release(this);
    }

    public void Launch(Vector2 direction)
    {
        rb.velocity = direction * bulletSpeed; // Sesuaikan arah peluru
    }


    // private void OnTriggerEnter2D(Collider2D other)
    // {
    //     HitboxComponent hitbox = other.GetComponent<HitboxComponent>();
    //     if (hitbox != null)
    //     {
    //         hitbox.Damage(damage); // Apply damage to the target
    //         pool?.Release(this);   // Return bullet to pool upon impact
    //     }
    // }

    // Reset the bullet when it's reused from the pool
    private void OnEnable()
    {
        rb.velocity = Vector2.zero; // Reset velocity to avoid unintended movement
    }

    private void OnDisable()
    {
        // Any additional reset logic if needed (e.g., trail effects or rotation)
        transform.rotation = Quaternion.identity; // Reset rotation if needed
    }
}
