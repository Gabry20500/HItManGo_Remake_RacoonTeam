using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] List<GameObject> objectsToDisable;
    [SerializeField] List<GameObject> objectToEnable;
    private AudioPlayer musicPlayer;

    private void Awake()
    {
        musicPlayer = FindObjectOfType<AudioPlayer>();
    }
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

    public void OpenOptions() 
    { 
        foreach(GameObject go in objectsToDisable)
        {
            go.SetActive(false);
        }
        foreach (GameObject go in objectToEnable)
        {
            go.SetActive(true);
        }
    }

    public void CloseOptions()
    {
        foreach (GameObject go in objectsToDisable)
        {
            go.SetActive(true);
        }
        foreach (GameObject go in objectToEnable)
        {
            go.SetActive(false);
        }
    }

    public void ShowCredits()
    {

    }

    public void MusicOnOff()
    {
        if(musicPlayer.GetComponent<AudioSource>().volume != 0.0f)
        {
            musicPlayer.GetComponent<AudioSource>().volume = 0.0f;
        }
        else
        {
            musicPlayer.GetComponent<AudioSource>().volume = 0.25f;
        }
    }
}
