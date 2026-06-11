using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
  public void playGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void LoginPage()
    {
        SceneManager.LoadScene("Login");
    }
    public void LeaderBoardPage()
    {
        SceneManager.LoadScene("Leaderbord");
    }
}
