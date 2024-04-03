using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1f;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void HandGame()
    {
        SceneManager.LoadScene("Handle");
    }

    public void TeamGame()
    {
        SceneManager.LoadScene("Team");
    }

    public void ReturnGame()
    {
        SceneManager.LoadScene("level00");
    }
}
