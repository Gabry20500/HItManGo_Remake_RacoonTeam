using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public bool canMove = true;
    public bool rockState = false;

    [SerializeField] public Node currentNode;

    [SerializeField] LevelManager lvlManager;
    [SerializeField] SwipeDetection swipeDetecter;


    [SerializeField] GameObject flyingRockObj;

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
            DisableMovement(); 

            foreach (Node node in currentNode.linkedNodes)
            {
                Vector3 nodeDir = node.position - transform.position;
                Vector2 nodeDir2D = new Vector2(nodeDir.x, nodeDir.z);

                if (Vector2.Dot(nodeDir2D.normalized, direction.normalized) > 0.85f)
                {
                    currentNode = node;
                    StartCoroutine(Animation(currentNode.position));
                }
            }
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

    public void Death()
    {
        SceneManager.LoadScene("LV_0");
    }

    public void TrhowRock(Node destination)
    {
        GameObject rock = Instantiate(flyingRockObj, transform.position, Quaternion.identity);
        rock.GetComponent<FlyingRock>().Init(destination);
    }

    public void EnableMovement()
    {
        canMove = true;
        swipeDetecter.OnSwipeDetected += Move;
    }

    public void DisableMovement()
    {
        canMove = false;
        swipeDetecter.OnSwipeDetected -= Move;
    }

    public void ActivateRockState()
    {
        rockState = true;
        canMove = false;
    }
    
    public void DisableRockState()
    {
        rockState = false;
        EnableMovement();
    }
}
