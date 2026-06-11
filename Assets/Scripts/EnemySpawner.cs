using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject[] enemyPrefabs;

    [Header("Attributes")]
    [SerializeField] private int baseEnemies = 8;
    [SerializeField] private float enemiesPerSecond = 0.5f;
    [SerializeField] private float spawnRateIncreasePerWave = 0.5f;
    [SerializeField] private float timeBetweenWaves = 5f;
    [SerializeField] private float difficultyScalingFactor = 0.75f;

    private int currentWave = 1;
    private float timeSinceLastSpawn;
    private int enemiesAlive;
    private int enemiesLeftToSpawn;
    private bool isSpawning = false;

    private float currentEnemiesPerSecond;

    public static EnemySpawner main;
    public int CurrentWave => currentWave;

    private void Awake()
    {
        main = this;
    }

    private void Start()
    {
        StartWave();
    }

    private void Update()
    {
        if (!isSpawning) return;

        timeSinceLastSpawn += Time.deltaTime;

        if (timeSinceLastSpawn >= (1f / currentEnemiesPerSecond) && enemiesLeftToSpawn > 0)
        {
            SpawnEnemy();

            enemiesLeftToSpawn--;
            enemiesAlive++;
            timeSinceLastSpawn = 0f;
        }

        if (enemiesAlive <= 0 && enemiesLeftToSpawn <= 0)
        {
            EndWave();
        }
    }

    public void EnemyDied()
    {
        enemiesAlive--;
    }

    private void StartWave()
    {
        isSpawning = true;
        enemiesLeftToSpawn = EnemiesPerWave();

        currentEnemiesPerSecond = enemiesPerSecond +
                                  ((currentWave - 1) * spawnRateIncreasePerWave);

        Debug.Log("Wave gestart: " + currentWave);
    }

    private void EndWave()
    {
        isSpawning = false;
        timeSinceLastSpawn = 0f;

        // 🔥 BELANGRIJK: sync wave naar GameManager
        if (GameManager.main != null)
        {
            GameManager.main.SetWave(currentWave);
        }

        currentWave++;

        Invoke(nameof(StartWave), timeBetweenWaves);
    }

    private void SpawnEnemy()
    {
        GameObject prefabToSpawn = enemyPrefabs[0];

        Instantiate(
            prefabToSpawn,
            LevelManager.main.startingPoint.position,
            Quaternion.identity
        );
    }

    private int EnemiesPerWave()
    {
        return Mathf.RoundToInt(
            baseEnemies * Mathf.Pow(currentWave, difficultyScalingFactor)
        );
    }
}