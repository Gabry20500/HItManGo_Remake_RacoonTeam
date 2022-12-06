using System.Collections.Generic;
using UnityEngine;

public class TemporaryScript : MonoBehaviour
{
    [SerializeField] List<Enemy> currentEnemies;
    [SerializeField] Node destination;

    private NodesPathFinding DFS;

    private void Awake()
    {
        DFS = new NodesPathFinding();
    }
    public void SetDestination()
    {
        foreach (Enemy enemy in currentEnemies)
        {
            List<Node> temp = DFS.Depth_First_Search(enemy.currentNode, destination);
            temp.Remove(temp[0]);
            enemy.pathToDestination = temp;
            enemy.destinationNode = destination;
        }    
    }
}
