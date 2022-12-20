using UnityEngine;

public class ClickablePlane : MonoBehaviour
{
    private bool unlocked = false;
    [SerializeField] private string levelName;

    private void OnMouseDown()
    {
        if (unlocked == true)
        {
            GameManager.instance.LoadLevel(levelName);
        }
    }

    public void Unlock()
    {
        unlocked = true;
    }
}
