using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void loadGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void loadMenu()
    {
        SceneManager.LoadScene("Menu Scene");
    }

    public void quitGame()
    {
        Application.Quit();
    }
     public void loadCredits()
    {
        SceneManager.LoadScene("Credits Scene");
    }
}

