using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private int hitPoints = 10;
    [SerializeField] private int currencyWorth = 20;

    private bool isDead = false; 

    public void TakeDamage(int dmg)
    {
        if (isDead) return; 

        hitPoints -= dmg;

        if (hitPoints <= 0)
        {
            isDead = true;

            LevelManager.main.IncreaseCurrency(currencyWorth);

            EnemySpawner.main.EnemyDied();

            Destroy(gameObject);
        }
    }
}