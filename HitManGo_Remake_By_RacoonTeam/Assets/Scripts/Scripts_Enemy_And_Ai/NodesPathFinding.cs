using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodesPathFinding : MonoBehaviour
{
    public Node startNode;
    public Node destinationNode;


    private List<Node> alreadyCrossed;

    public List<Node> bestPath;
    private List<Node> currentPath;



    public void FindPath()
    {
        alreadyCrossed.Add(startNode);

    }

    public void PathStep()
    {

    }



    public bool IsAlreadyCrossed(Node currentNode)
    {
        foreach(Node node in alreadyCrossed)
        {
            if (currentNode == node)
            {
                return true;
            }
            else continue;
        }

        return false;
    }
}
