using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public GameObject tutorialPanel; 

    public void StartGame()
    {
        SceneManager.LoadScene("Team_1"); 
    }

    public void ShowTutorial()
    {
        tutorialPanel.SetActive(true);
    }

    public void CloseTutorial()
    {
        tutorialPanel.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
