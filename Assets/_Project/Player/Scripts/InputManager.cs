using System;
using UnityEngine;
using UnityEngine.InputSystem;

/*
The purpose of this script is:
*/

[DefaultExecutionOrder(-1)]
public class InputManager : MonoBehaviour
{
    public Action OnTouchStarted, OnTouchEnded;

    public TouchControls TouchControls;



    private void Awake()
    {
        TouchControls = new TouchControls();
    }

    private void OnEnable()
    {
        TouchControls.Enable();
    }

    private void OnDisable()
    {
        TouchControls.Disable();
    }

    private void Start()
    {
        TouchControls.Touch.TouchPress.started += ctx => StartTouch(ctx);
        TouchControls.Touch.TouchPress.canceled += ctx => EndTouch(ctx);
    }

    void StartTouch(InputAction.CallbackContext context)
    {
        OnTouchStarted?.Invoke();
    }

    void EndTouch(InputAction.CallbackContext context)
    {
        OnTouchEnded?.Invoke();
    }

}
