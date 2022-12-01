using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool canMove = true;
    [SerializeField] private Node currentNode;

    [SerializeField] LevelManager lvlManager;
    [SerializeField] SwipeDetection swipeDetecter;

    private void Awake()
    {
        lvlManager = FindObjectOfType<LevelManager>();
        swipeDetecter = FindObjectOfType<SwipeDetection>();
    }

    private void OnEnable()
    {
        swipeDetecter.OnSwipeDetected += Move;
    }
    public void Move(Vector2 direction)
    {
        if (canMove)
        {
            canMove = false;
            swipeDetecter.OnSwipeDetected -= Move;  
            foreach (Node node in currentNode.linkedNodes)
            {
                Vector3 nodeDir = node.position - transform.position;
                Vector2 nodeDir2D = new Vector2(nodeDir.x, nodeDir.z);
                if (Vector2.Dot(nodeDir2D, direction) > 0.85f)
                {
                    currentNode = node;
                    StartCoroutine(Animation(currentNode.position));
                }
            }
            //lvlManager.UpdateLevel();
            StartCoroutine(Wait(1f));
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
        lvlManager.UpdateLevel();
    }
}
