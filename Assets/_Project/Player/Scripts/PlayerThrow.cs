using UnityEngine;

/*
The purpose of this script is:
*/

public class PlayerThrow : MonoBehaviour
{
    #region Variables

    ///***Serialized***///
    [SerializeField] Transform dummyTouch;
    [SerializeField] int throwSpeed;

    ///***Private***///
    bool amTouching;
    bool haveBeenThrown;

    Camera cam;
    Vector2 screenPos;
    InputManager inputManager;
    Vector3 screenCoordinates, worldCoordinates, difference;

    ///***Components***///
    Rigidbody2D myRb;
    Collider2D col;
    Transform myTransform;
    LineRenderer lineRenderer;

    #endregion



    private void Awake()
    {
        cam = Camera.main;
        amTouching = false;
        myTransform = transform;
        myRb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        col.enabled = false;
        inputManager = GetComponent<InputManager>();
        lineRenderer = transform.GetChild(0).GetComponent<LineRenderer>();
    }



    #region OnEnable/Disable

    private void OnEnable()
    {
        inputManager.OnTouchStarted += TouchStarted;
        inputManager.OnTouchEnded += TouchEnded;
        GameManager.Instance.OnGameLost += GameOver;
        GameManager.Instance.OnGameWin += GameOver;

        haveBeenThrown = false;
    }

    private void OnDisable()
    {
        inputManager.OnTouchStarted -= TouchStarted;
        inputManager.OnTouchEnded -= TouchEnded;
        GameManager.Instance.OnGameLost -= GameOver;
        GameManager.Instance.OnGameWin -= GameOver;

        col.enabled = false;
    }

    void GameOver()
    {
        gameObject.SetActive(false);
    }


    #endregion



    #region Update

    private void Update()
    {
        if (!amTouching) return;

        GetTouchPostion();
        
        RotateAroundTouch();
    }


    void GetTouchPostion()
    {
        screenPos = inputManager.TouchControls.Touch.TouchPosition.ReadValue<Vector2>();
        screenCoordinates = new Vector3(screenPos.x, screenPos.y, cam.nearClipPlane);
        worldCoordinates = cam.ScreenToWorldPoint(screenCoordinates);
        worldCoordinates.z = 0;

        dummyTouch.position = worldCoordinates;
    }


    void RotateAroundTouch()
    {
        difference = dummyTouch.position - myTransform.position;
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        if (10 <= rotationZ && rotationZ <= 160)
            myTransform.rotation = Quaternion.Euler(0, 0, rotationZ);
    }

    #endregion



    #region Input Events

    void TouchStarted()
    {
        if (haveBeenThrown) return; 
            
        amTouching = true;
        lineRenderer.enabled = true;
    }
    
    void TouchEnded()
    {
        amTouching = false;
        if (haveBeenThrown) return;
        
        myRb.velocity = transform.right * throwSpeed;
        haveBeenThrown = true;
        lineRenderer.enabled = false;
        col.enabled = true;
    }

    #endregion
}