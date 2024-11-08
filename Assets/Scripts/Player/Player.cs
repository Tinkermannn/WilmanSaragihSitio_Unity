using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Mengambil informasi dari Script PlayerMovement dan menyimpannya di playerMovement
    PlayerMovement playerMovement;

    // Animator pada EngineEffect
    Animator animator;

    // Satu Instance singleton
    public static Player Instance {
        get; // Properti Instance dapat dibaca dari luar kelas
        private set; // Diluar class tidak bisa merubah Instance
    }

    // Method Awake untuk inisialitazion 
    private void Awake() {
        // Mengecek jika sudah ada Instance Player lain, keep singleton (1 Instance)
        if (Instance != null && Instance != this) {
            Destroy(this);
        } else {
            Instance = this; // Disimpan
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
