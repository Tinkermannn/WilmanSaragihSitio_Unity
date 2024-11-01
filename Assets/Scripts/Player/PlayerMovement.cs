using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // [SerializeField] Memungkinkan pengaturan nilai variabel di editor Unity.
    [SerializeField] Vector2 maxSpeed;  // Kecepatan maksimum player pada tiap axis (x, y)
    [SerializeField] Vector2 timeToFullSpeed; // Waktu yang dibutuhkan untuk mencapai kecepatan penuh di tiap axis
    [SerializeField] Vector2 timeToStop;  // Waktu yang dibutuhkan player untuk berhenti sepenuhnya di tiap axis
    [SerializeField] Vector2 stopClamp; // Batas kecepatan minimum sebelum player dianggap berhenti di tiap axis

    Vector2 moveDirection; // Arah gerakan player berdasarkan input
    Vector2 moveVelocity; // Kecepatan tambahan untuk mencapai kecepatan maksimum dari gaya yang diberikan input 
    Vector2 moveFriction;  // Gaya gesek saat player bergerak
    Vector2 stopFriction; // Gaya gesek yang diterapkan saat player berhenti
    Rigidbody2D rb;  // Komponen Rigidbody2D untuk mengatur physics player


    void Start()
    {
        // Ambil komponen Rigidbody2D dari GameObject
        rb = GetComponent<Rigidbody2D>();

        // Gaya yang diberikan ke Player ketika diberikan input
        moveVelocity = 2 * maxSpeed / timeToFullSpeed;

        // Gaya gesek untuk mengurangi kecepatan selama pergerakan
        moveFriction = -2 * maxSpeed / (timeToFullSpeed * timeToFullSpeed);

        // Gaya gesek yang digunakan untuk memperlambat supaya player dapat berhenti
        stopFriction = -2 * maxSpeed / (timeToStop * timeToStop);
    }

    public void Move()
    {
        // Ambil input horizontal dan vertical dari user (API)
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        // Membuat Vektor Arah
        // Arah gerakan dari input user dan normalisasi agar nilainya tetap dalam skala
        moveDirection = new Vector2(inputX, inputY).normalized;

        // Atur gerakan sumbu X secara terpisah
        if (inputX != 0)
        {
            // Jika ada input horizontal, tambahkan kecepatan horizontal
            rb.velocity = new Vector2(
                rb.velocity.x + moveVelocity.x * inputX * Time.deltaTime, // Perubahan yang terjadi per frame akan dihitung berdasarkan waktu
                rb.velocity.y
            );
        }
        else
        {
            // Jika tidak ada input horizontal, tambahkan gaya gesek horizontal
            Vector2 frictionX = GetFriction() * new Vector2(1, 0); // Mengambil nilai gaya gesek pada sumbu x saja
            rb.velocity = new Vector2(
                Mathf.Abs(rb.velocity.x) < stopClamp.x ? 0 : rb.velocity.x + frictionX.x * Time.deltaTime, 
                rb.velocity.y
            );
        }

        // Atur gerakan sumbu Y secara terpisah
        if (inputY != 0)
        {
            // Jika ada input vertikal, tambahkan kecepatan vertikal
            rb.velocity = new Vector2(
                rb.velocity.x, 
                rb.velocity.y + moveVelocity.y * inputY * Time.deltaTime
            );
        }
        else
        {
            // Jika tidak ada input vertikal, tambahkan gaya gesek vertikal
            Vector2 frictionY = GetFriction() * new Vector2(0, 1); // Mengambil nilai gaya gesek pada sumbu y saja
            rb.velocity = new Vector2(
                rb.velocity.x, 
                Mathf.Abs(rb.velocity.y) < stopClamp.y ? 0 : rb.velocity.y + frictionY.y * Time.deltaTime
            );
        }

        // Membatasi kecepatan masing-masing sumbu menggunakan Mathf.Clamp agar tidak melebihi maxSpeed
        float clampedX = Mathf.Clamp(rb.velocity.x, -maxSpeed.x, maxSpeed.x);
        float clampedY = Mathf.Clamp(rb.velocity.y, -maxSpeed.y, maxSpeed.y);

        rb.velocity = new Vector2(clampedX, clampedY);
    }   

    // Fungsi untuk gaya gesek either saat bergerak atau berhenti
    Vector2 GetFriction()
    {
        // Return stopFriction saat tidak ada input, dan moveFriction saat ada input
        if (moveDirection == Vector2.zero) {
            return stopFriction * rb.velocity.normalized;
        } else {
            return moveFriction * moveDirection;
        }
    }

    public bool IsMoving()
    {
        // Return true jika kecepatan player sedang bergerak
        return rb.velocity.magnitude > 0.01f;
    }
}
