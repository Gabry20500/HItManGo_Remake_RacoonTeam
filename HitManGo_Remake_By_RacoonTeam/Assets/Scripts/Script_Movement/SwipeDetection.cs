using System;
using System.Collections;
using UnityEngine;

public class SwipeDetection : MonoBehaviour
{
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
            Debug.DrawLine(startPosition, endPosition, Color.gray, 1.0f);
            Vector3 direction = endPosition - startPosition;
            Vector2 direction2D = new Vector2(direction.x, direction.y).normalized;
            player.Move(direction2D);
        }
    }

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
