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
        myNode = GetComponentInParent<Node>();
    }


    private void OnMouseDown()
    {
        playerRef.TrhowRock(myNode);
        levelManager.DisableAllButton();
    }
}
