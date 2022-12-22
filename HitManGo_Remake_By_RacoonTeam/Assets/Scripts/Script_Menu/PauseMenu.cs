using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject pauseButton;

    public void Pause()
    {
        pauseMenu.SetActive(true);    
        pauseButton.SetActive(false);    
    }
    
    public void Resume()
    {
        pauseMenu.SetActive(false);
        pauseButton.SetActive(true);
    }

    public void GoBack()
    {
        SceneManager.LoadScene("LevelSelection");
        GameManager.instance.buttons.SetActive(true);
    }
}
