using System;
using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-1)]
public class InputManager : MonoBehaviour
{
    #region Events
    public delegate void StartTouch(Vector2 position, float time);
    public event StartTouch OnStartTouch;
    public delegate void EndTouch(Vector2 position, float time);
    public event EndTouch OnEndTouch;
    #endregion

    [SerializeField] private PlayerControl playerControl;
    private Camera mainCamera;


    private void Awake()
    {
        mainCamera = Camera.main;
        playerControl = new PlayerControl();
    }

    private void OnEnable()
    {
        playerControl.Enable();
    }

    private void OnDisable()
    {
        playerControl.Disable();
    }
    void Start()
    {
        playerControl.Touch.PrimaryContact.started += ctx => StartTouchPrimary(ctx);
        playerControl.Touch.PrimaryContact.canceled += ctx => EndTouchPrimary(ctx);
    }

    private void StartTouchPrimary(InputAction.CallbackContext ctx)
    {
        if(OnStartTouch != null)
        { 
            OnStartTouch(Utils.ScreenToWorld(mainCamera, playerControl.Touch.PrimaryPosition.ReadValue<Vector2>()), (float)ctx.startTime);           
        }
    }
    private void EndTouchPrimary(InputAction.CallbackContext ctx)
    {
        if (OnEndTouch != null)
        {
            OnEndTouch(Utils.ScreenToWorld(mainCamera, playerControl.Touch.PrimaryPosition.ReadValue<Vector2>()), (float)ctx.time);           
        }
    }

    public Vector2 PrimaryPosition()
    {
        return Utils.ScreenToWorld(mainCamera, playerControl.Touch.PrimaryPosition.ReadValue<Vector2>());
    }
}
