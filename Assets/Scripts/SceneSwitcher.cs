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
}
