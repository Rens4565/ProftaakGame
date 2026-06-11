using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class ScoreUploader : MonoBehaviour
{
    private const string URL =
        "http://localhost/proftaak/api/save_score.php";

    public void UploadScore(int waves, int score)
    {
        StartCoroutine(Send(waves));
    }

    IEnumerator Send(int waves)
    {
        int userId = PlayerPrefs.GetInt("UserId");

        WaveData data = new WaveData
        {
            user_id = userId,
            waves = waves
        };

        string json = JsonUtility.ToJson(data);

        UnityWebRequest request =
            new UnityWebRequest(URL, "POST");

        byte[] body = Encoding.UTF8.GetBytes(json);

        request.uploadHandler = new UploadHandlerRaw(body);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        Debug.Log(request.downloadHandler.text);
    }
}

[System.Serializable]
public class WaveData
{
    public int user_id;
    public int waves;
}