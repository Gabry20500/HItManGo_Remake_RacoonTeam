using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] Player playerRef;
    [SerializeField] SwipeDetection swipeDetecter;

    public List<Enemy> enemyInLevel;
    public Node[] nodesInLevel;
    [SerializeField] Node lastNode;

    private void Awake()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        foreach(Enemy e in enemies)
        {
            enemyInLevel.Add(e);
        }
        playerRef = FindObjectOfType<Player>();
        swipeDetecter = FindObjectOfType<SwipeDetection>();
        nodesInLevel = FindObjectsOfType<Node>();       
    }

    public void UpdateLevel()
    {
        foreach (Enemy enemy in enemyInLevel)
        {
            enemy.Move();
        }
        StartCoroutine(AllEnemyEnded());
        if(playerRef.currentNode == lastNode)
        {
            LevelCompleted();
        }
    }

    private void LevelCompleted()
    {
        SceneManager.LoadScene("OverWorld");
    }

    private IEnumerator AllEnemyEnded()
    {
        yield return StartCoroutine(CeckEnemy());

        playerRef.canMove = true;
        swipeDetecter.OnSwipeDetected += playerRef.Move;
    }

    private IEnumerator CeckEnemy()
    {
        uint i = 0;
        do
        {
            i = 0;
            foreach (Enemy enemy in enemyInLevel)
            {
                if (enemy.inMovement)
                {
                    i++;
                }
            }
            yield return null;
        } while (i > 0); 
    }


    public void DisableAllButton()
    {
        foreach(Node n in nodesInLevel)
        {
            n.DisableMyButton();
        }
    }
}
