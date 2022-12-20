using UnityEngine;
using System;
using System.Collections;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] SwipeDetection swipeDetecter;

    [SerializeField] private Transform target;
    [SerializeField] private float distanceToTarget;

    private Vector3 previousPos;
    private Vector3 newPos;
    private Vector3 dir;

    float rotationAroundYAxis;
    float rotationAroundXAxis;

    [SerializeField] float rotationSpeed = 30.0f;

    private void Awake()
    {
        swipeDetecter = FindObjectOfType<SwipeDetection>();
        distanceToTarget = (target.position - transform.position).magnitude;
    }

    private void OnEnable()
    {
        swipeDetecter.OnSwipeDetectedPoints += RotateCamera;
    }
    private void OnDisable()
    {
        swipeDetecter.OnSwipeDetectedPoints -= RotateCamera;
    }

    private void RotateCamera(Vector2 startPos, Vector2 endPos)
    {
        swipeDetecter.OnSwipeDetectedPoints -= RotateCamera;

        previousPos = Camera.main.ScreenToViewportPoint(startPos);
        newPos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        dir = previousPos - newPos;

        rotationAroundYAxis = -dir.x * 180; // camera moves horizontally
        rotationAroundXAxis = dir.y * 180; // camera moves vertically

        

        StartCoroutine(RotationMovement());
        //Camera.main.transform.Rotate(new Vector3(1, 0, 0), rotationAroundXAxis);
        //Camera.main.transform.Rotate(new Vector3(0, 1, 0), rotationAroundYAxis, Space.World); 

        //Camera.main.transform.Translate(new Vector3(0, 0, -distanceToTarget));
    }

    //Sistemare swipe dir e limitaizoni
    private IEnumerator RotationMovement()
    {
        float xRot = 0.0f;
        float yRot = 0.0f;

        while(xRot < rotationAroundXAxis || yRot < rotationAroundYAxis)
        {
            Camera.main.transform.position = target.position;
            if (xRot < rotationAroundXAxis)
            {
                xRot += rotationSpeed * Time.deltaTime;
                Camera.main.transform.Rotate(new Vector3(1, 0, 0), rotationSpeed * Time.deltaTime);
            }
            if (yRot < rotationAroundYAxis)
            {
                yRot += rotationSpeed * Time.deltaTime;
                Camera.main.transform.Rotate(new Vector3(0, 1, 0), rotationSpeed * Time.deltaTime, Space.World);
            }
            Camera.main.transform.Translate(new Vector3(0, 0, -distanceToTarget));
            yield return null;
        }
        
        swipeDetecter.OnSwipeDetectedPoints += RotateCamera;
    }
}
