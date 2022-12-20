using UnityEngine;
using UnityEngine.SceneManagement;

public class ClickablePlane : MonoBehaviour
{
    private bool unlocked = false;
    [SerializeField] private string levelName;

    private void OnMouseDown()
    {
        if (unlocked == true)
        {
            SceneManager.LoadScene(levelName);
        }
    }

    public void Unlock()
    {
        unlocked = true;
    }
}
