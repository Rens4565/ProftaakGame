using System.Collections;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class LoginManager : MonoBehaviour
{
    public TMP_InputField emailInput;
    public TMP_InputField passwordInput;
    public TMP_Text statusText;

    private const string LOGIN_URL =
        "http://localhost/proftaak/api/login.php";

    public void Login()
    {
        Debug.Log("LOGIN BUTTON GECHECKT");
        StartCoroutine(LoginCoroutine());
    }

    IEnumerator LoginCoroutine()
    {
        LoginRequest data = new LoginRequest
        {
            email = emailInput.text,
            password = passwordInput.text
        };

        string json = JsonUtility.ToJson(data);

        UnityWebRequest request =
            new UnityWebRequest(LOGIN_URL, "POST");

        byte[] bodyRaw = Encoding.UTF8.GetBytes(json);

        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();

        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        Debug.Log("HTTP CODE: " + request.responseCode);
        Debug.Log("ERROR: " + request.error);
        Debug.Log("RESPONSE: " + request.downloadHandler.text);

        if (request.result == UnityWebRequest.Result.Success)
        {
            LoginResponse response =
                JsonUtility.FromJson<LoginResponse>(
                    request.downloadHandler.text
                );

            if (response.success)
            {
                statusText.text = "Login gelukt";

                // user opslaan
                PlayerPrefs.SetInt("UserId", response.user.id);
                PlayerPrefs.SetString("username", response.user.username);

                Debug.Log("UserId opgeslagen: " + response.user.id);

                SceneManager.LoadScene("MainMenu");
            }
            else
            {
                statusText.text = "Login mislukt";
            }
        }
        else
        {
            statusText.text = "Server fout";
            Debug.Log(request.error);
        }
    }
}

[System.Serializable]
public class LoginRequest
{
    public string email;
    public string password;
}

[System.Serializable]
public class LoginResponse
{
    public bool success;
    public UserData user;
}

[System.Serializable]
public class UserData
{
    public int id;
    public string username;
    public string email;
    public string role;
}