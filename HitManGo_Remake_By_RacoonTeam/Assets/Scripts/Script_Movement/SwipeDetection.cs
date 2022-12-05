using System;
using UnityEngine;

public class SwipeDetection : MonoBehaviour
{
    public delegate void SwipeDetected(Vector2 direction);
    public event SwipeDetected OnSwipeDetected;


    [SerializeField] float minimumDistance = 0.2f;
    [SerializeField] float maximumTime = 1.0f;
  
    private InputManager inputManager;

    private Vector2 startPosition;
    private float startTime;
    private Vector2 endPosition;
    private float endTime;
    [SerializeField, Range(0.0f, 1.0f)] private float directionThreshold = 0.7f;

    Vector3 lastDirection;
    [SerializeField] Player player;

    [SerializeField] GameObject a;


    private void Awake()
    {
        inputManager = FindObjectOfType<InputManager>();
    }

    private void OnEnable()
    {
        inputManager.OnStartTouch += SwipeStart;
        inputManager.OnEndTouch += SwipeEnd;
    }

    private void OnDisable()
    {
        inputManager.OnStartTouch -= SwipeStart;
        inputManager.OnEndTouch -= SwipeEnd;
    }

    private void SwipeStart(Vector2 position, float time)
    {
        startPosition = position;
        startTime = time;
    }

    private void SwipeEnd(Vector2 position, float time)
    {
        endPosition = position;
        endTime = time;
        DetectSwipe();
    }

    private void DetectSwipe()
    {
        Debug.Log(startPosition);
        if(Vector3.Distance(startPosition, endPosition) >= minimumDistance && 
            endTime - startTime <= maximumTime)
        {
            Debug.DrawLine(startPosition, endPosition, Color.yellow, 100.0f);
            Vector3 direction = endPosition - startPosition;
            
            Ray ray = Camera.main.ScreenPointToRay(startPosition);
            RaycastHit hit;

            
            if(Physics.Raycast(ray, out hit))
            {
                //Debug.Log($"Player hit");
                Instantiate(a, hit.point, Quaternion.identity);
                if ( hit.collider.CompareTag("Player"))
                {
                    //Debug.Log($"Player hit");
                    OnSwipeDetected(direction);
                }
            }
            Debug.DrawRay(startPosition, Camera.main.transform.forward, Color.red);
        }
    }
    
    
    
    // var ray = new Ray(Camera.main.ScreenToViewportPoint(startPosition), Camera.main.transform.forward);
    //             RaycastHit hit;
    //             
    //             if(Physics.Raycast(Camera.main.ScreenToViewportPoint(startPosition), Camera.main.transform.forward, out hit, Mathf.Infinity))
    //             {
    //                 Instantiate(a, hit.point, Quaternion.identity);
    //                 if ( hit.collider.CompareTag("Player"))
    //                 {
    //                     OnSwipeDetected(direction);
    //                 }
    //             }

    //private void SwipeDirection(Vector2 direction)
    //{
        //lastDirection = new Vector3(direction.x, 0.0f, direction.y);
        //player.transform.forward = lastDirection;

        //Debug.Log("Swipe detected");
        //if(Vector2.Dot(Vector2.up, direction) > directionThreshold)
        //{
        //    //Debug.Log("Swipe straight");
        //}
        //else if (Vector2.Dot(new Vector2(0.50f, 0.50f), direction) > directionThreshold)
        //{
        //    Debug.Log("Swipe light right");
        //}
        //else if(Vector2.Dot(new Vector2(0.75f, 0.25f), direction) > directionThreshold)
        //{
        //    Debug.Log("Swipe full right(dux)");
        //}
        //else if(Vector2.Dot(new Vector2(-0.50f, -0.50f), direction) > directionThreshold)
        //{
        //    Debug.Log("Swipe light left");
        //}
        //else if(Vector2.Dot(new Vector2(-0.75f, -0.25f), direction) > directionThreshold)
        //{
        //    Debug.Log("Swipe full left");
        //}
    //}
}
