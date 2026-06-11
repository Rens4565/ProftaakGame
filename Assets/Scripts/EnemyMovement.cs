using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Attributes")]
    [SerializeField] private float moveSpeed = 2f;

    [SerializeField] private float speedIncreasePerWave = 0.2f;

    private Transform target;
    private int pathIndex = 0;

    private void Start()
    {
        target = LevelManager.main.path[pathIndex];

        if (EnemySpawner.main != null)
        {
            moveSpeed += (EnemySpawner.main.CurrentWave - 1) * speedIncreasePerWave;
        }
    }

    private void Update()
    {
        if (Vector2.Distance((Vector2)target.position, rb.position) < 0.1f)
        {
            pathIndex++;

            if (pathIndex >= LevelManager.main.path.Length)
            {
                GameManager.main.TakeDamage(1);

                EnemySpawner.main.EnemyDied();

                Destroy(gameObject);
                return;
            }

            target = LevelManager.main.path[pathIndex];
        }
    }

    private void FixedUpdate()
    {
        Vector2 direction = ((Vector2)target.position - rb.position).normalized;

        rb.linearVelocity = direction * moveSpeed;
    }
}