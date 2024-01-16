using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CreateFloor : Prefabricate
{
    //Debuger
    public static ILogger debugFloor = new Logger(Debug.unityLogger.logHandler);
  
    private MousePosition mousePositionScript;
    private Vector3 offset;


    private GameObject floor;
    private bool floorIsDraged = false;
    private bool floorIsAtached = false;
    private bool floorIsAtachedToX = false;
    private bool floorIsAtachedToZ = false;
    private Vector3 position;
    private float positionX;
    private float positionZ;

    private void Start()
    {
        //Debugger
        debugFloor.logEnabled = false;

        objectId = 2;
        SetDimensions();
        SetPosition();
        mousePositionScript = GameObject.Find("User Input Manager").GetComponent<MousePosition>();
    }


    public override void SetDimensions()
    {
        gameObject.transform.localScale = new Vector3(x, 0.16f, z);
    }

    public override void SetPosition()
    {
        transform.position = new Vector3(transform.position.x, 0.08f, transform.position.z);
    }

    private void OnMouseDown()
    {
        floorIsAtached = false;
        floorIsAtachedToX = false;
        floorIsAtachedToZ = false;
        offset = transform.position - mousePositionScript.mousePosition;
    }

    private void OnMouseDrag()
    {
        floorIsDraged = true;
        mousePositionScript.mouseDragsObject = true;
        //move anywhere
        if (!floorIsAtached)
        {
            transform.position = mousePositionScript.mousePosition + offset;
        }
        else if (floorIsAtached)
        {
            //move only on x axis
            if (floorIsAtachedToX) 
            {
                position = new Vector3(transform.position.x, gameObject.transform.localScale.y / 2, positionZ);
                transform.position = position;
            }
            //move only on Z axis
            else if (floorIsAtachedToZ) {
                position = new Vector3(positionX, gameObject.transform.localScale.y / 2, transform.position.z);
                transform.position = position;
            }
        }
    }

    private void OnMouseUp()
    {
        floorIsDraged = false;
        mousePositionScript.mouseDragsObject = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        //Floor collide with floor
        if (other.transform.CompareTag("Floor") && floorIsDraged)
        {
            floorIsAtached = true;
            floor = other.gameObject;
            //X values
            float floorSizeX = floor.transform.localScale.x;
            float floorPositionX = floor.transform.position.x;
            float rightFloorBound = floorPositionX - floorSizeX / 2 - gameObject.transform.localScale.x / 2;
            float leftFloorBound = floorPositionX + floorSizeX / 2 + gameObject.transform.localScale.x / 2;
            //Z values
            float floorSizeZ = floor.transform.localScale.z;
            float floorPositionZ = floor.transform.position.z;
            float backFloorBound = floorPositionZ - floorSizeZ / 2 - gameObject.transform.localScale.z / 2;
            float frontFloorBound = floorPositionZ + floorSizeZ / 2 + gameObject.transform.localScale.z / 2;

            float distanceBetweenPositionsX = Mathf.Round(Mathf.Abs(gameObject.transform.position.x - floorPositionX));
            float distanceRegardingSizeX = ((gameObject.transform.localScale.x + floorSizeX)/ 2);

            float distanceBetweenPositionsZ = Mathf.Round(Mathf.Abs(gameObject.transform.position.z - floorPositionZ));
            float distanceRegardingSizeZ = ((gameObject.transform.localScale.z + floorSizeZ) / 2);

            if (distanceBetweenPositionsX >= distanceRegardingSizeX || distanceBetweenPositionsX <= distanceRegardingSizeX + 1)
            {
                floorIsAtachedToZ = true;
                //Collides from right side of floor
                if (gameObject.transform.position.x < floorPositionX)
                {
                    positionX = rightFloorBound;
                    position = new Vector3(rightFloorBound, gameObject.transform.localScale.y / 2, transform.position.z);
                    gameObject.transform.position = position;
                }
                //Collides from left side of floor
                else if (gameObject.transform.position.x > floorPositionX)
                {
                    positionX = leftFloorBound;
                    mousePositionScript.mousePosition.x = leftFloorBound;
                    position = new Vector3(leftFloorBound, gameObject.transform.localScale.y / 2, transform.position.z);
                    gameObject.transform.position = position;
                }
            }

            else if (distanceBetweenPositionsZ >= distanceRegardingSizeZ || distanceBetweenPositionsZ <= distanceRegardingSizeZ + 1)
            {
                floorIsAtachedToX = true;
                //Collides with floor from behind
                if (gameObject.transform.position.z < floorPositionZ)
                {
                    positionZ = backFloorBound;
                    position = new Vector3(transform.position.x, gameObject.transform.localScale.y / 2, frontFloorBound);
                   // mousePositionScript.mousePosition.z = backFloorBound;
                    gameObject.transform.position = new Vector3(transform.position.x, gameObject.transform.localScale.y / 2, backFloorBound);
                }
                //Collides with floor from front
                else if (gameObject.transform.position.z > frontFloorBound)
                {
                    positionZ = frontFloorBound;
                    position = new Vector3(transform.position.x, gameObject.transform.localScale.y / 2, frontFloorBound);
                    mousePositionScript.mousePosition.z = frontFloorBound;
                    gameObject.transform.position = new Vector3(transform.position.x, gameObject.transform.localScale.y / 2, frontFloorBound);
                }
            }
        }
    }

}
