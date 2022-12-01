using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] public Node currentNode;
    [SerializeField] public List<Node> pathToDestination;
    [SerializeField] public Node destinationNode;
    public bool inMovement = false;
    
    public void Move()
    {
        if (currentNode != destinationNode)
        {
            inMovement = true;
            Node nextNode = pathToDestination[0];
            pathToDestination.Remove(nextNode);
            StartCoroutine(Animation(nextNode.position));
            currentNode = nextNode;
        }
    }


    private IEnumerator Animation(Vector3 destination)
    {
        Vector3 startPos = transform.position;
        Vector3 endPos = new Vector3(destination.x, transform.position.y, destination.z);

        Vector3 dir = (endPos + startPos) / 2;
        Vector3 midPos = new Vector3(dir.x, (dir.y + 10f), dir.z);

        yield return StartCoroutine(goMidPos(startPos, midPos));
        yield return StartCoroutine(goEndPos(midPos, endPos));

        transform.position = endPos;
        inMovement = false;
        yield return null;
    }

    private IEnumerator goMidPos(Vector3 startPos, Vector3 midPos)
    {
        float elapsed = 0f;
        float duration = 0.5f;

        while (elapsed < duration)
        {
            float perc = elapsed / duration;

            transform.position = Vector3.Lerp(startPos, midPos, perc);
            elapsed += Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator goEndPos(Vector3 midPos, Vector3 endPos)
    {
        float elapsed = 0f;
        float duration = 0.5f;

        while (elapsed < duration)
        {
            float perc = elapsed / duration;

            transform.position = Vector3.Lerp(midPos, endPos, perc);
            elapsed += Time.deltaTime;
            yield return null;
        }
    }
    
    private IEnumerator Wait(float time)
    {

        yield return new WaitForSeconds(time);
    }
}
