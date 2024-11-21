using UnityEngine;
using UnityEngine.Pool;

public class Weapon : MonoBehaviour
{
    [Header("Weapon Stats")]
    [SerializeField] private float shootIntervalInSeconds = 3f; // Interval waktu antara tembakan dalam detik

    [Header("Bullets")]
    [SerializeField] private Bullet bulletPrefab; // Prefab untuk bullet yang akan ditembakkan
    [SerializeField] private Transform bulletSpawnPoint; // Titik spawn untuk bullet

    [Header("Bullet Pool")]
    private IObjectPool<Bullet> bulletPool; // Pool objek untuk menyimpan bullet yang tidak aktif

    private float timer; // Timer untuk menghitung interval tembakan

    // Tambahan field sesuai permintaan
    private readonly bool collectionCheck = false; // Cek apakah koleksi bisa dipilih
    private readonly int defaultCapacity = 30; // Kapasitas default pool
    private readonly int maxSize = 100; // Ukuran maksimum pool
    public Transform parentTransform; // Transform parent untuk bullet (opsional)

    // Inisialisasi bullet pool dan timer saat game dimulai
    private void Awake()
    {
        // Inisialisasi bullet pool dengan parameter yang diperlukan
        bulletPool = new ObjectPool<Bullet>(
            CreateBullet,  // Untuk membuat bullet baru
            OnTakeBulletFromPool,  // Untuk mengambil bullet dari pool
            OnReturnBulletToPool,  // Untuk mengembalikan bullet ke pool
            OnDestroyBullet,  // Untuk menghancurkan bullet
            collectionCheck,  // Cek koleksi
            defaultCapacity,  // Kapasitas default pool
            maxSize  // Ukuran maksimum pool
        );

        // Set timer untuk memungkinkan tembakan segera setelah game mulai
        timer = shootIntervalInSeconds;
    }

    private void Update()
    {
        // Menambahkan waktu pada timer setiap frame dan cek jika interval tembak telah tercapai
        timer += Time.deltaTime;
        if (timer >= shootIntervalInSeconds)
        {
            Shoot();  // Tembakkan bullet
            timer = 0f;  // Reset timer
        }
    }

    // Fungsi untuk menembakkan bullet
    private void Shoot()
    {
        if (bulletPool != null)
        {
            // Ambil bullet dari pool dan atur posisinya di spawn point bullet
            Bullet bullet = bulletPool.Get();
            bullet.transform.position = bulletSpawnPoint.position;
            bullet.transform.rotation = bulletSpawnPoint.rotation;  // Atur rotasi bullet sesuai spawn point
            bullet.Launch();  // Aktivasi gerakan bullet
        }
    }

    // Fungsi untuk membuat bullet baru menggunakan prefab
    private Bullet CreateBullet()
    {
        Bullet newBullet = Instantiate(bulletPrefab);  // Instantiate bullet baru dari prefab
        newBullet.SetPool(bulletPool);  // Pass referensi pool ke bullet agar bisa digunakan kembali
        newBullet.gameObject.SetActive(false);  // Set bullet dalam keadaan tidak aktif terlebih dahulu
        return newBullet;
    }

    // Fungsi yang dipanggil saat bullet diambil dari pool
    private void OnTakeBulletFromPool(Bullet bullet)
    {
        bullet.gameObject.SetActive(true);  // Aktifkan bullet ketika diambil dari pool
    }

    // Fungsi yang dipanggil saat bullet dikembalikan ke pool
    private void OnReturnBulletToPool(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);  // Matikan bullet ketika dikembalikan ke pool
    }

    // Fungsi yang dipanggil saat bullet dihancurkan
    private void OnDestroyBullet(Bullet bullet)
    {
        Destroy(bullet.gameObject);  // Hancurkan objek bullet
    }
}
