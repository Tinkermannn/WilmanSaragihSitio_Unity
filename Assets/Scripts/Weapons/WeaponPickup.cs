using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField] Weapon weaponHolder;  // Referensi ke prefab weapon yang akan dipickup oleh player
    Weapon weapon; // Instance dari weapon yang diambil oleh player

    void Awake()
    {
        // Weapon diinisialisasi sebagai null
        weapon = null;
    }

    void Start()
    {
        // Deactive visual weapon
        TurnVisual(false);
    }

    // Fungsi yang dipanggil ketika objek masuk ke collider dari WeaponPickup
    void OnTriggerEnter2D(Collider2D other)
    {
        // Cek apakah objek yang menyentuh adalah objek dengan Player (tagnya)
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player try to take the weapon!");

            if (weaponHolder == null) // Cek weaponHolder either sudah diassign atau belum di Inspector dengan weapon yang sesuai
            {
                Debug.LogError("WeaponHolder tidak diassign: " + gameObject.name);
                return;
            }

            // Cek apakah Player sudah memiliki weapon atau belum
            Weapon playerWeapon = other.GetComponentInChildren<Weapon>();
            if (playerWeapon != null)
            {
                // Matikan visual weapon lama dan hancurkan instance-nya
                TurnVisual(false, playerWeapon);
                Destroy(playerWeapon.gameObject);
                Debug.Log("weapon sebelumnya dinonaktifkan dan dihapus.");
            }

            // Instance baru dari weaponHolder dan set parent ke Player
            weapon = Instantiate(weaponHolder, other.transform); 
            weapon.transform.localPosition = Vector3.zero; 

            // Aktifkan visual weapon baru
            TurnVisual(true, weapon);

            Debug.Log("weapon baru berhasil diambil oleh Player!");
        }
    }

    // Set visual weapon
    void TurnVisual(bool on)
    {
        // Cek apakah weapon instance tidak null, lalu aktifkan atau nonaktifkan
        if (weapon != null)
        {
            weapon.gameObject.SetActive(on);
        }
    }

    // Set visual weapon dengan weapon yang spesifik
    void TurnVisual(bool on, Weapon weapon)
    {
        if (weapon != null)
        {
            weapon.gameObject.SetActive(on);
        }
    }
}
