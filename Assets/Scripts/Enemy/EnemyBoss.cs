using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EnemyBoss : Enemy
{
    [Header("Enemy Boss Stats")]
    public float shootInterval = 1f; // Interval between each shot
    public float speed = 3f; // Boss movement speed
    private float shootTimer;
    private Vector2 direction;

    [Header("Bullet Pool")]
    [SerializeField] private Bullet bulletPrefab; // Bullet prefab
    [SerializeField] private Transform bulletSpawnPoint; // Make sure this is correctly set in the prefab hierarchy
    private IObjectPool<Bullet> bulletPool;

    void Awake()
    {
        // Initialize the bullet pool
        bulletPool = new ObjectPool<Bullet>(
            CreateBullet,
            OnGetBullet,
            OnReleaseBullet,
            OnDestroyBullet,
            collectionCheck: false,
            defaultCapacity: 10,
            maxSize: 20
        );
    }

    void Start()
    {
        // Initialize the shoot timer
        shootTimer = shootInterval;

        // Tentukan posisi spawn dan arah gerakan berdasarkan sisi layar
        float screenWidth = Camera.main.orthographicSize * Camera.main.aspect;
        Vector2 spawnPosition;

        if (Random.value < 0.5f)
        {
            // Spawn dari sisi kiri layar
            spawnPosition = new Vector2(-screenWidth - 1, Random.Range(-Camera.main.orthographicSize, Camera.main.orthographicSize));
            direction = Vector2.right; // Gerak ke kanan
        }
        else
        {
            // Spawn dari sisi kanan layar
            spawnPosition = new Vector2(screenWidth + 1, Random.Range(-Camera.main.orthographicSize, Camera.main.orthographicSize));
            direction = Vector2.left; // Gerak ke kiri
        }

        transform.position = spawnPosition;
    }

    void Update()
    {
        // Pindahkan EnemyBoss sesuai arah
        transform.Translate(direction * speed * Time.deltaTime);

        // Update the shoot timer
        shootTimer -= Time.deltaTime;
        if (shootTimer <= 0f)
        {
            Shoot();
            shootTimer = shootInterval; // Reset the timer
        }

        // Cek apakah sudah mencapai batas layar untuk membalik arah
        float screenWidth = Camera.main.orthographicSize * Camera.main.aspect;
        if (Mathf.Abs(transform.position.x) > screenWidth + 1)
        {
            // Balik arah jika melewati batas layar
            direction = -direction;
        }
    }

    void Shoot()
    {
        // Get a bullet from the pool and shoot it
        var bullet = bulletPool.Get();
        bullet.transform.position = bulletSpawnPoint.position;
        bullet.transform.rotation = bulletSpawnPoint.rotation;
        bullet.Launch(Vector2.down);
    }

    Bullet CreateBullet()
    {
        // Instantiate a new bullet and set it up with the pool
        var bullet = Instantiate(bulletPrefab);
        bullet.SetPool(bulletPool);
        return bullet;
    }

    void OnGetBullet(Bullet bullet)
    {
        // Enable the bullet when retrieved from the pool
        bullet.gameObject.SetActive(true);
    }

    void OnReleaseBullet(Bullet bullet)
    {
        // Disable the bullet when returned to the pool
        bullet.gameObject.SetActive(false);
    }

    void OnDestroyBullet(Bullet bullet)
    {
        // Destroy the bullet object when it's no longer needed
        Destroy(bullet.gameObject);
    }
}
