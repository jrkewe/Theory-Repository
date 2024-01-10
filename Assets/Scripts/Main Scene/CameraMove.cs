
    using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UIElements;

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

    private float speed = 2.5f;

    private void Start()
    {
        ZoomCamera = Camera.main;
        mousePositionScript = GameObject.Find("User Input Manager").GetComponent<MousePosition>();
    }

    void Update()
    {
        //MovementForward();
        DragCamera();
        Zoom();
        RotateCamera();
    }

    void RotateCamera() 
    {
        if (Input.GetMouseButton(1)) 
        {
            transform.eulerAngles += new Vector3(-Input.GetAxis("Mouse Y"), -Input.GetAxis("Mouse X"), 0) * speed;
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
            startPoint = mousePositionScript.mousePoition;
        }
        if (Input.GetMouseButton(0))
        {
            endPoint = mousePositionScript.mousePoition;
            lengthOfDrag = endPoint - startPoint;

            transform.Translate (new Vector3(-lengthOfDrag.x, -lengthOfDrag.y, -lengthOfDrag.z) * Time.deltaTime * speed, Space.World);
        }

    }
}
