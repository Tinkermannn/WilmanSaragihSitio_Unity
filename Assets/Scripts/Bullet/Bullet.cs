using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Pool;

public class Bullet : MonoBehaviour
{
    [Header("Bullet Stats")]
    public float bulletSpeed = 20f; 
    public int damage = 10; 

    private Rigidbody2D rb; 
    private IObjectPool<Bullet> pool; // Referensi ke pool untuk mengelola objek bullet

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>(); // Mengambil komponen Rigidbody2D pada objek bullet
    }

    // Menyimpan referensi pool untuk pengelolaan bullet
    public void SetPool(IObjectPool<Bullet> bulletPool)
    {
        pool = bulletPool; // Menetapkan pool bullet
    }

    // Meluncurkan bullet dengan kecepatan yang sudah ditentukan, arah ditentukan oleh rotasi bullet
    public void Launch()
    {
        rb.velocity = transform.up * bulletSpeed; // Mengatur kecepatan bullet berdasarkan arah dan kecepatan
    }

    // Saat bullet keluar dari layar, kembalikan bullet ke pool
    private void OnBecameInvisible()
    {
        pool?.Release(this); // Mengembalikan bullet ke pool jika tidak terlihat di layar
    }

    public void Launch(Vector2 direction)
    {
        rb.velocity = direction * bulletSpeed; // Sesuaikan arah bullet dengan parameter direction
    }

    // Saat objek bullet diaktifkan, reset kecepatan bullet
    private void OnEnable()
    {
        rb.velocity = Vector2.zero; // Reset kecepatan bullet agar tidak bergerak secara tidak sengaja
    }

    // Saat objek bullet dinonaktifkan, reset rotasi bullet
    private void OnDisable()
    {
        transform.rotation = Quaternion.identity; // Reset rotasi bullet
    }

    public int GetDamage()
    {
        return damage; 
    }

    private void OnTriggerEnter2D(Collider2D other)
{
    // Only process collision if the bullet is not from the player's own collider
    if (other.gameObject != gameObject.transform.parent?.gameObject)
    {
        // Check specifically for Enemy tag
        if (other.CompareTag("Enemy"))
        {
            HealthComponent health = other.GetComponent<HealthComponent>();

            if (health != null)
            {
                health.Subtract(damage); // Damage only enemies
            }

            // Return bullet to pool after collision
            pool?.Release(this);
        }
    }
}
}