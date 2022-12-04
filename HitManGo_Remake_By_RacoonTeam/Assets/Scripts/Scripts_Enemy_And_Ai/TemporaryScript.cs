using System.Collections.Generic;
using UnityEngine;

public class TemporaryScript : MonoBehaviour
{
    [SerializeField] Enemy currentEnemy;
    [SerializeField] Node destination;

    private NodesPathFinding DFS;

    private void Awake()
    {
        DFS = new NodesPathFinding();
    }
    public void SetDestination()
    {
        List<Node> temp = DFS.Depth_First_Search(currentEnemy.currentNode, destination);
        temp.Remove(temp[0]);
        currentEnemy.pathToDestination = temp;
        currentEnemy.destinationNode = destination;
    }
}
