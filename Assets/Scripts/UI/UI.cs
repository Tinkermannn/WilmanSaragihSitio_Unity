using UnityEngine;
using UnityEngine.UIElements;

public class UI : MonoBehaviour
{
    private VisualElement rootElement;
    private Label waveLabel;
    private Label enemiesLeftLabel;
    private Label pointsLabel;
    private Label healthLabel;

    [SerializeField] private CombatManager combatManager;
    [SerializeField] private HealthComponent playerHealth;

    private int totalPoints = 0;
    private static UI instance; // Singleton instance

    void Awake()
    {
        // Set up singleton
        instance = this;
    }

    void OnEnable()
    {
        var uiDocument = GetComponent<UIDocument>();
        rootElement = uiDocument.rootVisualElement;

        // Get references to UI elements
        waveLabel = rootElement.Q<Label>("WaveLabel");
        enemiesLeftLabel = rootElement.Q<Label>("EnemiesLeftLabel");
        pointsLabel = rootElement.Q<Label>("PointsLabel");
        healthLabel = rootElement.Q<Label>("HealthLabel");

        // Get references if not assigned in inspector
        if (combatManager == null)
            combatManager = FindObjectOfType<CombatManager>();
            
        if (playerHealth == null)
            playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthComponent>();

        UpdateUI();
    }

    void Update()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (combatManager != null)
        {
            waveLabel.text = $"Wave: {combatManager.waveNumber}";
            int activeEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length;
            enemiesLeftLabel.text = $"Enemies Left: {activeEnemies}";
        }

        if (playerHealth != null)
        {
            healthLabel.text = $"Health: {playerHealth.GetHealth()}";
        }

        pointsLabel.text = $"Points: {totalPoints}";
    }

    // Static method untuk menambah points
    public static void AddPoints(int enemyLevel)
    {
        if (instance != null)
        {
            instance.totalPoints += enemyLevel;
            Debug.Log($"Points added: {enemyLevel}. Total points: {instance.totalPoints}");
        }
    }

    public static int GetTotalPoints()
    {
        return instance != null ? instance.totalPoints : 0;
    }
}