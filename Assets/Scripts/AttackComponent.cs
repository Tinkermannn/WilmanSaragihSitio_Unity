using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class AttackComponent : MonoBehaviour
{
    // Properti untuk bullet dan damage
    [SerializeField] private Bullet bullet;
    [SerializeField] private int damage = 10;

    private void Start()
    {
        // Pastikan collider objek ini diatur sebagai trigger
        Collider2D collider = GetComponent<Collider2D>();
        if (collider != null)
        {
            collider.isTrigger = true;
        }
        else
        {
            Debug.LogError("Collider tidak ditemukan pada objek ini.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Cek apakah objek yang bertabrakan memiliki tag yang sama
        if (other.CompareTag(gameObject.tag))
        {
            return; // Jika tag sama, abaikan tabrakan
        }

        // Cek apakah objek yang bertabrakan memiliki HitboxComponent
        HitboxComponent hitbox = other.GetComponent<HitboxComponent>();
        if (hitbox != null)
        {
            // Jika HitboxComponent ditemukan, panggil metode Damage pada objek tersebut
            hitbox.Damage(damage);
        }
    }
}
