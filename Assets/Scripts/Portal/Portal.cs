using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] float speed;           // Kecepatan pergerakan asteroid portal
    [SerializeField] float rotateSpeed;     // Kecepatan rotasi asteroid portal
    Vector2 newPosition;                    // Posisi target baru bagi asteroid

    void Start()
    {
        // Inisialisasi posisi target baru menggunakan ChangePosition
        ChangePosition();
    }

    void Update()
    {
        // Memindahkan asteroid ke newPosition dengan kecepatan yang ditentukan
        transform.position = Vector2.MoveTowards(transform.position, newPosition, speed * Time.deltaTime);

        // Rotate asteroid
        transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);

        // Memeriksa jika jarak antara posisi saat ini dan newPosition kurang dari 0.5
        if (Vector2.Distance(transform.position, newPosition) < 0.5f)
        {
            // Update posisi target yang baru kalau asteroid mendekati newPosition
            ChangePosition();
        }

        // Memeriksa apakah Player memiliki senjata
        GameObject player = GameObject.FindWithTag("Player");
        bool hasWeapon = player != null && player.GetComponentInChildren<Weapon>() != null;

        // Mengaktifkan atau menonaktifkan renderer dan collider asteroid berdasarkan apakah Player memiliki senjata
        GetComponent<SpriteRenderer>().enabled = hasWeapon;
        GetComponent<Collider2D>().enabled = hasWeapon;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Jika objek yang bertabrakan adalah Player
        if (other.CompareTag("Player"))
        {
            Debug.Log("Asteroid collides with Player."); // Debug untuk deteksi tabrakan
            FindObjectOfType<LevelManager>().LoadScene("Main"); // Memuat scene "Main"
        }
    }

    void ChangePosition()
    {
        // Menentukan posisi acak baru untuk asteroid dalam batas tertentu pada sumbu X
        float randomX = Random.Range(-5f,5f);   // Menentukan batas acak pada sumbu X
        float fixedY = transform.position.y;       // Menyimpan posisi Y saat ini agar tetap
        newPosition = new Vector2(randomX, fixedY); // Menetapkan posisi target baru dengan X acak dan Y tetap
    }
}
