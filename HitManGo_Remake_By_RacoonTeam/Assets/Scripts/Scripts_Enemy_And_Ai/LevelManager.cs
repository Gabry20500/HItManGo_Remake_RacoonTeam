using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public Player playerRef;
    public List<Enemy> enemyInLevel;


    public void UpdateLevel()
    {
        foreach(Enemy enemy in enemyInLevel)
        {
            enemy.Move();
        }
    }
}
