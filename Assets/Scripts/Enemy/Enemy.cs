using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Stats")]
    [SerializeField] public int level = 1; // Level musuh

    private Rigidbody2D rb; // Referensi Rigidbody2D
    protected Transform playerTransform; // Referensi ke Transform player
    public UnityEvent enemyKilledEvent; // Event untuk musuh yang dihancurkan

    private void Awake()
    {
        // Mendapatkan komponen Rigidbody2D
        rb = GetComponent<Rigidbody2D>();

        // Mengatur agar Rigidbody2D tidak terpengaruh gravitasi
        if (rb != null)
        {
            rb.gravityScale = 0f;
        }

        // Inisialisasi UnityEvent jika null
        enemyKilledEvent ??= new UnityEvent();
    }

    private void Start()
    {
        // Menemukan player berdasarkan tag "Player"
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
    }

    private void Update()
    {
        // Menghadap ke player
        FacePlayer();
    }

    private void FacePlayer()
    {
        if (playerTransform != null)
        {
            // Menghitung arah ke player
            Vector2 direction = playerTransform.position - transform.position;

            // Menghitung rotasi yang diperlukan untuk menghadap player
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // Mengatur rotasi musuh
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
    }

    public void SetLevel(int level)
    {
        this.level = level;
    }

    public int GetLevel()
    {
        return level;
    }

  private void OnDestroy()
    {
        // Invoke event sebelum object dihancurkan
        if (enemyKilledEvent != null)
        {
            UI.AddPoints(level);
            enemyKilledEvent.Invoke();
        }
    }
    
}
