
    using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    private float horizontalInput;
    private float verticalInput;
    private float speed = 5.0f;
    private Vector2 scrollInput;
    private float mouseRotationX = 0.0f;
    private float mouseRotationY = 0.0f;

    //Drag camera
    private Vector3 Origin;
    private Vector3 Difference;

    private bool drag;

    private MousePosition mousePositionScript;

    private void Start()
    {
        mousePositionScript = GameObject.Find("User Input Manager").GetComponent<MousePosition>();
    }

    void Update()
    {
        Debug.Log("Mouse move");
        if (Input.GetMouseButtonDown(0)) 
        {
            Difference = Camera.main.ScreenToWorldPoint(mousePositionScript.mousePoition) - Camera.main.transform.position;
            if (drag == false)
            {
                drag = true;
                Origin = Camera.main.ScreenToWorldPoint(mousePositionScript.mousePoition);
            }
            else 
            {
                drag = false;
            }

            if (drag) 
            {
            Camera.main.transform.position = Origin-Difference;
            }
        }

        //    horizontalInput = Input.GetAxis("Horizontal");
        //    verticalInput = Input.GetAxis("Vertical");
        //    scrollInput = Input.mouseScrollDelta;

        //    transform.Translate(Vector3.forward * speed * scrollInput.y * 0.1f);

        //    //transform.Translate(Vector3.forward * speed * verticalInput);

        //    //transform.Translate(Vector3.right * speed * horizontalInput);
        //    // transform.Rotate(Vector3.up, speed * horizontalInput);
        //    mouseRotationX += Input.GetAxis("Mouse X") * 15.0f * (-1);
        //    mouseRotationY += Input.GetAxis("Mouse Y") * 15.0f;
        //    transform.localEulerAngles = new Vector3 (mouseRotationX, mouseRotationY, 0);

    }

}
