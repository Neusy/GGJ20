using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneSwitcher : MonoBehaviour
{

    public void GoToSettings()
    {
        SceneManager.LoadScene("Settings");
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void GoToMainLevel()
    {
        SceneManager.LoadScene("MainLevel");
    }
    public void GoToMainCredits()
    {
        SceneManager.LoadScene("Credits");
    }
    public void GoToControls()
    {
        SceneManager.LoadScene("Controls");
    }

    public void quit()
    {
        Application.Quit();
    }

}
