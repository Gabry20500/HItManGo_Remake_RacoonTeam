using System.Collections.Generic;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] Player playerRef;
    [SerializeField] SwipeDetection swipeDetecter;

    public List<Enemy> enemyInLevel;
    public Node[] nodesInLevel;

    private void Awake()
    {
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
}
