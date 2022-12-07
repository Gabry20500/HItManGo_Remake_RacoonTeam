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
        if(Vector3.Distance(startPosition, endPosition) >= minimumDistance && 
            endTime - startTime <= maximumTime)
        {
            Vector3 direction = endPosition - startPosition;
           
            Ray startRay = Camera.main.ScreenPointToRay(startPosition);
            Ray endRay = Camera.main.ScreenPointToRay(endPosition);

            RaycastHit startHit;
            Physics.Raycast(startRay, out startHit);
            RaycastHit endHit;
            Physics.Raycast(endRay, out endHit);

            Vector3 swipeDir = endHit.point - startHit.point;
            Vector2 swipeDir2D = new Vector2(swipeDir.x, swipeDir.z);

            if (startHit.collider != null)
            {
                if (startHit.collider.CompareTag("Player"))
                {
                    OnSwipeDetected(swipeDir2D);
                }
            }
        }
    }
        
}
