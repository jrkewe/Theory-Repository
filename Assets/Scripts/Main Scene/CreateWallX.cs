using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateWallX : Prefabricate
{
    private MousePosition mousePositionScript;
    private bool wallCollided = false;
    private Vector3 position;
    private Vector3 offset;
    GameObject floor;

    private void Start()
    {
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
        offset = transform.position - mousePositionScript.mousePoition;
    }

    private void OnMouseDrag()
    {

        float wallXHeight = gameObject.transform.localScale.y / 2;

        //WallX draged anywhere
        if (!wallCollided)
        {
            Debug.Log("WallX is outside floor");
            gameObject.transform.position = new Vector3(mousePositionScript.mousePoition.x, wallXHeight, mousePositionScript.mousePoition.z + offset.z);
        }
        //WallX collided with floor
        else if (floor!=null && wallCollided)
        {
            float floorSizeZ = floor.transform.localScale.z;
            float floorPositionZ = floor.transform.position.z;
            float backFloorBound = floorPositionZ - (floorSizeZ / 2) + (transform.localScale.z / 2);
            float frontFloorBound = floorPositionZ + (floorSizeZ / 2) - (transform.localScale.z / 2);
            Debug.Log("Back: " +backFloorBound);
            Debug.Log("Front: " +frontFloorBound);

            //WallX hits back of floor edge
            if (gameObject.transform.position.z < backFloorBound)
            {
                Debug.Log("WallX hits back of floor edge");
                Debug.Log("Position: " + gameObject.transform.position);
                gameObject.transform.position = new Vector3(transform.position.x, wallXHeight, backFloorBound);
            }

            //WallX hits front of floor edge
            else if (gameObject.transform.position.z > frontFloorBound) 
            {
                Debug.Log("WallX hits front of floor edge");
                Debug.Log("Position: " + gameObject.transform.position);
                gameObject.transform.position = new Vector3(transform.position.x, wallXHeight, frontFloorBound);
            }

            //WallX in the middle of floor edge
            else if (gameObject.transform.position.z <= frontFloorBound && gameObject.transform.position.z >= backFloorBound)
            {

                mousePositionScript.mousePoition.z = Mathf.Clamp(mousePositionScript.mousePoition.z, backFloorBound, frontFloorBound);
                Debug.Log("WallX in the middle of floor edge");
                Debug.Log("Position: " +gameObject.transform.position);
                gameObject.transform.position = new Vector3(transform.position.x, wallXHeight, mousePositionScript.mousePoition.z);
            }
            
        }

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
                mousePositionScript.mousePoition.x = rightFloorBound;
                position = new Vector3(rightFloorBound, y / 2, transform.position.z);
                gameObject.transform.position = position;
            }
            //Collides from left side of floor
            else if (gameObject.transform.position.x > leftFloorBound)
            {
                mousePositionScript.mousePoition.x = leftFloorBound;
                position = new Vector3(leftFloorBound, y / 2, transform.position.z);
                gameObject.transform.position = position;
            }
        }
    }

}

