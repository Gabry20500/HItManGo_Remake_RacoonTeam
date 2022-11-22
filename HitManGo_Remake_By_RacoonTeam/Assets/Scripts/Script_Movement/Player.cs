using UnityEngine;

public class Player : MonoBehaviour
{
    public bool canMove = true;
    [SerializeField] private Node currentNode;

    public void Move(Vector2 direction)
    {
        foreach (Node node in currentNode.linkedNodes)
        {
            Vector3 nodeDir = node.position - transform.position;
            Vector2 nodeDir2D = new Vector2(nodeDir.x, nodeDir.z);
            if (Vector2.Dot(nodeDir2D, direction) > 0.8f)
            {
                currentNode = node;
                transform.position = currentNode.position;
            }
        }
    }
}
