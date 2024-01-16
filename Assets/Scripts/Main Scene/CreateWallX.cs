using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateWallX : Prefabricate
{
    //Debuger
    public static ILogger debugWallX = new Logger(Debug.unityLogger.logHandler);

    private MousePosition mousePositionScript;
    private bool wallCollided = false;
    private Vector3 position;
    private Vector3 offset;
    private GameObject floor;

    private void Start()
    {
        //Debugger
        debugWallX.logEnabled = false;

        objectId = 0;
        SetDimensions();
        SetPosition();
        mousePositionScript = GameObject.Find("User Input Manager").GetComponent<MousePosition>();
    }

    public override void SetDimensions()
    {
        transform.localScale = new Vector3(0.25f, y, z);
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
        float wallXHeight = gameObject.transform.localScale.y / 2;

        //WallX draged anywhere
        if (!wallCollided)
        {
            debugWallX.Log("WallX is outside floor");
            gameObject.transform.position = new Vector3(mousePositionScript.mousePosition.x + offset.x, wallXHeight, mousePositionScript.mousePosition.z + offset.z);
        }
        //WallX collided with floor
        else if (floor!=null && wallCollided)
        {
            float floorSizeZ = floor.transform.localScale.z;
            float floorPositionZ = floor.transform.position.z;
            float backFloorBound = floorPositionZ - (floorSizeZ / 2) + (transform.localScale.z / 2);
            float frontFloorBound = floorPositionZ + (floorSizeZ / 2) - (transform.localScale.z / 2);
            debugWallX.Log("Back: " +backFloorBound);
            debugWallX.Log("Front: " +frontFloorBound);

            //WallX hits back of floor edge
            if (gameObject.transform.position.z < backFloorBound)
            {
                debugWallX.Log("WallX hits back of floor edge");
                debugWallX.Log("Position: " + gameObject.transform.position);
                gameObject.transform.position = new Vector3(transform.position.x, wallXHeight, backFloorBound);
            }

            //WallX hits front of floor edge
            else if (gameObject.transform.position.z > frontFloorBound) 
            {
                debugWallX.Log("WallX hits front of floor edge");
                debugWallX.Log("Position: " + gameObject.transform.position);
                gameObject.transform.position = new Vector3(transform.position.x, wallXHeight, frontFloorBound);
            }

            //WallX in the middle of floor edge
            else if (gameObject.transform.position.z <= frontFloorBound && gameObject.transform.position.z >= backFloorBound)
            {

                mousePositionScript.mousePosition.z = Mathf.Clamp(mousePositionScript.mousePosition.z, backFloorBound, frontFloorBound);
                debugWallX.Log("WallX in the middle of floor edge");
                debugWallX.Log("Position: " + gameObject.transform.position);
                gameObject.transform.position = new Vector3(transform.position.x, wallXHeight, mousePositionScript.mousePosition.z);
            }

        }

    }

    private void OnMouseUp()
    {
        mousePositionScript.mouseDragsObject = false;

    }

    private void OnTriggerEnter(Collider other)
    {
        //WallX collide with floor
        if (other.transform.CompareTag("Floor"))
        {
            wallCollided = true;
            floor = other.gameObject;
            float floorSizeX = floor.transform.localScale.x;
            float floorPositionX = floor.transform.position.x;
            float rightFloorBound = floorPositionX - floorSizeX / 2 + 0.125f;
            float leftFloorBound = floorPositionX + floorSizeX / 2 - 0.125f;

            //Collides from right side of floor
            if (gameObject.transform.position.x < floorPositionX)
            {
                mousePositionScript.mousePosition.x = rightFloorBound;
                position = new Vector3(rightFloorBound, gameObject.transform.localScale.y/2, transform.position.z);
                gameObject.transform.position = position;
            }
            //Collides from left side of floor
            else if (gameObject.transform.position.x > leftFloorBound)
            {
                mousePositionScript.mousePosition.x = leftFloorBound;
                position = new Vector3(leftFloorBound, gameObject.transform.localScale.y / 2, transform.position.z);
                gameObject.transform.position = position;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        wallCollided = false;
    }


}

