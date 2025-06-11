using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("SolsterWorld");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
