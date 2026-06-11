using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class LeaderboardManager : MonoBehaviour
{
    [SerializeField] private TMP_Text leaderboardText;

    private const string URL =
        "http://localhost/proftaak/api/get_scores.php?type=waves&limit=50";

    public void LoadLeaderboard()
    {
        StartCoroutine(Fetch());
    }

    IEnumerator Fetch()
    {
        UnityWebRequest request = UnityWebRequest.Get(URL);

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            string json = request.downloadHandler.text;
            Debug.Log(json);

           
            LeaderboardWrapper wrapper =
                JsonUtility.FromJson<LeaderboardWrapper>(json);

            string output = "";

            if (wrapper.data != null)
            {
                for (int i = 0; i < wrapper.data.Length; i++)
                {
                    output += (i + 1) + ". " +
                              wrapper.data[i].username +
                              " - " +
                              wrapper.data[i].waves_survived +
                              " waves\n";
                }
            }

            leaderboardText.text = output;
        }
        else
        {
            Debug.LogError(request.error);
        }
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}

[System.Serializable]
public class LeaderboardWrapper
{
    public LeaderboardItem[] data;
}

[System.Serializable]
public class LeaderboardItem
{
    public string username;
    public int waves_survived;
    public int score;
}

