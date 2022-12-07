using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] public Node currentNode;
    [SerializeField] public List<Node> pathToDestination;
    [SerializeField] public Node destinationNode;
    public bool inMovement = false;



    [SerializeField] LevelManager lvlManager;


    private void Awake()
    {
        lvlManager = FindObjectOfType<LevelManager>();
    }
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
        Vector3 targetDirection = destination - transform.position;
        Quaternion buffer = Quaternion.LookRotation(targetDirection);
        transform.rotation = new Quaternion(transform.rotation.x, buffer.y, transform.rotation.z, buffer.w);

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

    private IEnumerator KillPlayer(Collider other)
    {
        yield return new WaitForSeconds(2f);
        other.GetComponent<Player>().Death();
    }

    public void RotateTo(Node destination)
    {
        Vector3 targetDirection = destination.position - transform.position;
        Quaternion buffer = Quaternion.LookRotation(targetDirection);
        transform.rotation = new Quaternion(transform.rotation.x, buffer.y, transform.rotation.z, buffer.w);
    }
    
    
    
    
    private void OnTriggerEnter(Collider other)
    {        
        if(other.gameObject.CompareTag("Player"))
        {
            other.enabled = false;
            StartCoroutine(Animation(other.GetComponent<Player>().currentNode.position));
            StartCoroutine(KillPlayer(other));
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            lvlManager.enemyInLevel.Remove(this);
            Destroy(this.gameObject);
        }
    }
}
