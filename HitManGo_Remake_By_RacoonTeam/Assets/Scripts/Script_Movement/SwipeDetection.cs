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


    private void Awake()
    {
        inputManager = FindObjectOfType<InputManager>();
        player = FindObjectOfType<Player>();
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
        if(Vector3.Distance(startPosition, endPosition) >= minimumDistance && 
            endTime - startTime <= maximumTime)
        {
            Vector3 direction = endPosition - startPosition;
           
            Ray ray = Camera.main.ScreenPointToRay(startPosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit))
            {
                if ( hit.collider.CompareTag("Player"))
                {
                    OnSwipeDetected(direction);
                }
            }
        }
    }
        
}
