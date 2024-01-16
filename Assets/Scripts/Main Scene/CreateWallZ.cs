using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateWallZ : Prefabricate
{
    //Debuger
    public static ILogger debugWallZ = new Logger(Debug.unityLogger.logHandler);

    private MousePosition mousePositionScript;
    private Vector3 offset;
    private bool wallCollided = false;
    private GameObject floor;
    private Vector3 position;

    private void Start()
    {
        //Debugger
        debugWallZ.logEnabled = false;

        objectId = 1;
        SetDimensions();
        SetPosition();
        mousePositionScript = GameObject.Find("User Input Manager").GetComponent<MousePosition>();
    }

    public override void SetDimensions()
    {
        transform.localScale = new Vector3(x, y, 0.25f);
    }

    public override void SetPosition()
    {
        position = new Vector3(transform.position.x, y / 2, transform.position.z);
        gameObject.transform.position = position;
    }

    private void OnMouseDown()
    {
        offset = transform.position - mousePositionScript.mousePosition;
    }

    private void OnMouseDrag()
    {

        mousePositionScript.mouseDragsObject = true;
        float wallZHeight = gameObject.transform.localScale.y / 2;

        //WallZ draged anywhere
        if (!wallCollided)
        {
            debugWallZ.Log("WallZ is outside floor");
            gameObject.transform.position = new Vector3(mousePositionScript.mousePosition.x + offset.x, wallZHeight, mousePositionScript.mousePosition.z + offset.z);
        }
        //WallZ collided with floor
        else if (floor != null && wallCollided)
        {
            float floorSizeX = floor.transform.localScale.x;
            float floorPositionX = floor.transform.position.x;
            float rightFloorBound = floorPositionX - (floorSizeX / 2) + (transform.localScale.x / 2);
            float leftFloorBound = floorPositionX + (floorSizeX / 2) - (transform.localScale.x / 2);
            debugWallZ.Log("Right: " + rightFloorBound);
            debugWallZ.Log("Left: " + leftFloorBound);

            //WallZ hits right side of floor edge
            if (gameObject.transform.position.x < rightFloorBound)
            {
                debugWallZ.Log("WallZ hits right floor edge");
                debugWallZ.Log("Position: " + gameObject.transform.position);
                gameObject.transform.position = new Vector3(rightFloorBound, wallZHeight, transform.position.z);
            }

            //WallZ hits left side of floor edge
            else if (gameObject.transform.position.x > leftFloorBound)
            {
                debugWallZ.Log("WallZ hits left floor edge");
                debugWallZ.Log("Position: " + gameObject.transform.position);
                gameObject.transform.position = new Vector3(leftFloorBound, wallZHeight, transform.position.z);
            }

            //WallZ in the middle of floor edge
            else if (gameObject.transform.position.x <= leftFloorBound && gameObject.transform.position.x >= rightFloorBound)
            {

                mousePositionScript.mousePosition.x = Mathf.Clamp(mousePositionScript.mousePosition.x, rightFloorBound, leftFloorBound);
                debugWallZ.Log("WallZ in the middle of floor edge");
                debugWallZ.Log("Position: " + gameObject.transform.position);
                gameObject.transform.position = new Vector3(mousePositionScript.mousePosition.x, wallZHeight, transform.position.z);
            }

        }
    }

    private void OnMouseUp()
    {
        mousePositionScript.mouseDragsObject = false;

    }

    private void OnTriggerEnter(Collider other)
    {
        //WallZ collide with floor
        if (other.transform.CompareTag("Floor"))
        {

            debugWallZ.Log("Collide with floor");
            wallCollided = true;
            floor = other.gameObject;
            float floorSizeZ = floor.transform.localScale.z;
            float floorPositionZ = floor.transform.position.z;
            float backFloorBound = floorPositionZ - floorSizeZ / 2 + 0.125f;
            float frontFloorBound = floorPositionZ + floorSizeZ / 2 - 0.125f;

            //Collides with floor from behind

            if (gameObject.transform.position.z < floorPositionZ)
            {
                debugWallZ.Log("Uderza z tylu");
                mousePositionScript.mousePosition.z = backFloorBound;
                position = new Vector3(transform.position.x, gameObject.transform.localScale.y / 2, backFloorBound);
                gameObject.transform.position = position;
            }
            //Collides with floor from front
            else if (gameObject.transform.position.z > frontFloorBound)
            {
                debugWallZ.Log("Uderza z przodu");
                mousePositionScript.mousePosition.z = frontFloorBound;
                position = new Vector3(transform.position.x, gameObject.transform.localScale.y / 2, frontFloorBound);
                gameObject.transform.position = position;
            }
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        wallCollided = false;
    }

}
