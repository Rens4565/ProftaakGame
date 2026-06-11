using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager main;

    [SerializeField] private int playerHealth = 10;

    public int currentWave;
    public ScoreUploader scoreUploader;

    private void Awake()
    {
        main = this;
    }

    private void Start()
    {
        currentWave = 0;
    }

    public void SetWave(int wave)
    {
        currentWave = wave;
        Debug.Log("Current wave opgeslagen: " + currentWave);
    }

    public void TakeDamage(int damage)
    {
        playerHealth -= damage;

        if (playerHealth <= 0)
        {
            StartCoroutine(GameOverRoutine());
        }
    }

    private IEnumerator GameOverRoutine()
    {
        Debug.Log("GAME OVER");

        int wave = currentWave;

        if (scoreUploader != null)
        {
            Debug.Log("Uploading score... wave = " + wave);
            scoreUploader.UploadScore(wave, 0);

            yield return new WaitForSeconds(1.0f); // wacht upload
        }
        else
        {
            Debug.LogError("ScoreUploader is NULL");
        }

        SceneManager.LoadScene("GameOver");
    }
}