using UnityEngine;
using UnityEngine.Pool;

public class Weapon : MonoBehaviour
{
    [Header("Weapon Stats")]
    [SerializeField] private float shootIntervalInSeconds = 3f;

    [Header("Bullets")]
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private Transform bulletSpawnPoint;

    [Header("Bullet Pool")]
    private IObjectPool<Bullet> bulletPool;

    private float timer;

    private void Awake()
    {
        // Initialize the bullet pool with required parameters
        bulletPool = new ObjectPool<Bullet>(
            CreateBullet,
            OnTakeBulletFromPool,
            OnReturnBulletToPool,
            OnDestroyBullet,
            collectionCheck: false,
            defaultCapacity: 30,
            maxSize: 100
        );

        // Set timer to allow immediate shooting on game start
        timer = shootIntervalInSeconds;
    }

    private void Update()
    {
        // Increment the timer and check if it has reached the shoot interval
        timer += Time.deltaTime;
        if (timer >= shootIntervalInSeconds)
        {
            Shoot();
            timer = 0f;
        }
    }

    private void Shoot()
    {
        if (bulletPool != null)
        {
            // Get bullet from the pool and set its position and rotation
            Bullet bullet = bulletPool.Get();
            bullet.transform.position = bulletSpawnPoint.position;
            bullet.transform.rotation = bulletSpawnPoint.rotation;
            bullet.Launch();  // Activate bullet movement
        }
    }

private Bullet CreateBullet()
{
    if (bulletPrefab == null)
    {
        Debug.LogError("Bullet prefab is not assigned!");
        return null;
    }
    
    Bullet newBullet = Instantiate(bulletPrefab);
    newBullet.SetPool(bulletPool);  // Pass pool reference for reuse
    newBullet.gameObject.SetActive(false); // Start bullets as inactive
    return newBullet;
}


    private void OnTakeBulletFromPool(Bullet bullet)
    {
        bullet.gameObject.SetActive(true);
    }

    private void OnReturnBulletToPool(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
    }

    private void OnDestroyBullet(Bullet bullet)
    {
        Destroy(bullet.gameObject);
    }
}
