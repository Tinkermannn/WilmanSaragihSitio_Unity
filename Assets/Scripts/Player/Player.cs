using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Mengambil informasi dari Script PlayerMovement dan menyimpannya di playerMovement
    PlayerMovement playerMovement;

    // Animator pada EngineEffect
    Animator animator;

    // Satu instance singleton
    public static Player instance {
        get; // Properti instance dapat dibaca dari luar kelas
        private set; // Diluar class tidak bisa merubah instance
    }

    // Method Awake untuk inisialitazion 
    private void Awake() {
        // Mengecek jika sudah ada instance Player lain, keep singleton (1 instance)
        if (instance != null && instance != this) {
            Destroy(this);
        } else {
            instance = this; // Disimpan
        }
    }

    // Dijalankan sekali pada awal game
    void Start()
    {
        // Mendapatkan komponen PlayerMovement dari GameObject
        playerMovement = GetComponent<PlayerMovement>();

        // Find GameObject dengan nama "EngineEffect" untuk animation
        GameObject engineEffect = GameObject.Find("EngineEffect");
        if (engineEffect != null) {
            // Jika ditemukan, ambil komponen Animator dari EngineEffect
            animator = engineEffect.GetComponent<Animator>();
        } else {
            // jika EngineEffect tidak ditemukan, berikan log warning
            Debug.LogWarning("EngineEffect tidak ditemukan");
        }
    }

    // FixedUpdate dijalankan pada interval tetap untuk physics frames koordinat 
    // Waktu yang konsisten 
    void FixedUpdate()
    {
        // Memanggil method Move dari PlayerMovement untuk menggerakkan player
        playerMovement.Move();
    }

    // LateUpdate dijalankan setelah semua update selesai
    void LateUpdate()
    {
        // Mengatur parameter "IsMoving" di animator berdasarkan apakah player bergerak
        animator.SetBool("IsMoving", playerMovement.IsMoving());
    }
}
