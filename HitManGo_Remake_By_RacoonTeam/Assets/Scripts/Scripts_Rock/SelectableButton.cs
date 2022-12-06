using UnityEngine;

public class SelectableButton : MonoBehaviour
{
    #region Variables

    [SerializeField] Node myNode;
    [SerializeField] Player playerRef;
    [SerializeField] LevelManager levelManager;

    #endregion
    private void Awake()
    {
        playerRef = FindObjectOfType<Player>();
        levelManager = FindObjectOfType<LevelManager>();
    }


    private void OnMouseDown()
    {
        playerRef.TrhowRock(myNode);
        levelManager.DisableAllButton();
    }
}
