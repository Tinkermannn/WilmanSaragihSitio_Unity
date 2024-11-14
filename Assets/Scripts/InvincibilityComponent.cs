using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(HitboxComponent))]
public class InvincibilityComponent : MonoBehaviour
{
    [SerializeField] private int blinkingCount = 7;
    [SerializeField] private float blinkInterval = 0.1f;
    [SerializeField] private Material blinkMaterial;
    
    private SpriteRenderer spriteRenderer;
    private Material originalMaterial;
    public bool isInvincible = false;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalMaterial = spriteRenderer.material;
    }

    // Coroutine untuk menjalankan efek blinking
    private IEnumerator BlinkingCoroutine()
    {
        isInvincible = true;

        for (int i = 0; i < blinkingCount; i++)
        {
            spriteRenderer.material = blinkMaterial; // Ganti material ke blink
            yield return new WaitForSeconds(blinkInterval);
            spriteRenderer.material = originalMaterial; // Kembali ke material asli
            yield return new WaitForSeconds(blinkInterval);
        }

        isInvincible = false;
    }

    // Method untuk memulai invincibility jika belum invincible
    public void StartInvincibility()
    {
        if (!isInvincible)
        {
            StartCoroutine(BlinkingCoroutine());
        }
    }
}
