using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void GoToScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        Debug.Log(sceneName+ " has been loaded.");
    }

    public void QuitApp()
    {
        Application.Quit();
        Debug.Log("Game has quit.");
    }
}
