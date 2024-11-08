using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    // Animator untuk mengontrol animasi transisi
    [SerializeField] private Animator animator;

    void Awake()
    {
        // Memastikan animator ter-assign jika belum
        if (animator == null)
        {
            animator = GetComponent<Animator>();  // Ambil komponen Animator dari objek
        }
    }

    // Coroutine untuk load scene asynchronous
    IEnumerator LoadSceneAsync(string sceneName)
    {
        // Play animasi StartTransition 
        animator.SetTrigger("Start");

        yield return new WaitForSeconds(1);  //Delay transisi

        // Load scene baru secara asynchronous
        var asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        while (!asyncLoad.isDone)  // Menunggu hingga scene selesai dimuat
        {
            yield return null;  // Tunggu hingga scene selesai dimuat
        }

        // Mencari kamera utama dengan tag "MainCamera" di scene
        Camera mainCam = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();

        // Mengambil semua kamera yang ada di scene
        Camera[] cameras = Camera.allCameras;
        
        // Menghancurkan semua kamera yang tidak memiliki tag "MainCamera"
        foreach (Camera cam in cameras)
        {
            if (cam != mainCam)  // Jika kamera bukan kamera yang harus dipertahankan
            {
                Destroy(cam.gameObject);  // Hancurkan kamera tersebut
            }
        }

        // Menempatkan kembali posisi pemain di koordinat tertentu setelah scene dimuat
        Player.Instance.transform.position = new Vector2(0,0);

        // Memainkan animasi transisi akhir SCALE 20
        animator.SetTrigger("End");
    }

    // Load scene baru
    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneAsync(sceneName));  // Memulai coroutine untuk memuat scene secara asynchronous
    }
}
