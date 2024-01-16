using UnityEngine;
using UnityEngine.EventSystems;

public class CameraMove : MonoBehaviour
{
    //Camera position change
    private float horizontalInput;
    private float verticalInput;

    //Zoom camera
    private Camera ZoomCamera;
    private float scrollSpeed = 10f;

    //Drag camera
    private Vector3 startPoint;
    private Vector3 endPoint;
    private Vector3 lengthOfDrag;
    private MousePosition mousePositionScript;

    private float speed = 0.5f;

    private void Start()
    {
        ZoomCamera = Camera.main;
        mousePositionScript = GameObject.Find("User Input Manager").GetComponent<MousePosition>();
    }

    void Update()
    {
        //MovementForward();
         
        if (!mousePositionScript.mouseDragsObject && !PointerIsOverUI())  
        {
            DragCamera();
        }
        Zoom();
        RotateCamera();
    }

    void RotateCamera() 
    {
        if (Input.GetMouseButton(1)) 
        {
            transform.eulerAngles += new Vector3(-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0) * speed;
        }
    }

    void Zoom() 
    {
        ZoomCamera.fieldOfView -= Input.GetAxis("Mouse ScrollWheel") * scrollSpeed;
    }

    void MovementForward()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        gameObject.transform.Translate(Vector3.right * horizontalInput * speed);
        gameObject.transform.Translate(Vector3.forward * verticalInput * speed);
    }

    void DragCamera()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            mousePositionScript.mouseIsDraged = true;
            startPoint = mousePositionScript.mousePosition;
        }
        if (Input.GetMouseButton(0))
        {
            endPoint = mousePositionScript.mousePosition;
            lengthOfDrag = endPoint - startPoint;

            transform.Translate (new Vector3(-lengthOfDrag.x, -lengthOfDrag.y, -lengthOfDrag.z) * Time.deltaTime * speed, Space.World);
        }
        if (Input.GetMouseButtonUp(0)) 
        {
            mousePositionScript.mouseIsDraged = false;
        }

    }

    private bool PointerIsOverUI()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return true;
        }
        else 
        { 
            return false;
        }
    }

}
