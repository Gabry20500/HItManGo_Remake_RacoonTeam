using System.Collections.Generic;
using System.Collections;
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
        StartCoroutine(MoveAllEnemies());
    }

    public IEnumerator MoveAllEnemies()
    {
        foreach (Enemy enemy in enemyInLevel)
        {
            enemy.Move();
        }
        swipeDetecter.OnSwipeDetected += playerRef.Move;
        yield return null;
    }
}
