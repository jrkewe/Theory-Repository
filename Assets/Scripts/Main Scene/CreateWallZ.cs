using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateWallZ : Prefabricate
{
    private MousePosition mousePositionScript;
    private Vector3 offset;
    private bool wallCollided = false;
    GameObject floor;
    private Vector3 position;

    private void Start()
    {
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
        offset = transform.position - mousePositionScript.mousePoition;
    }

    private void OnMouseDrag()
    {
        float wallZHeight = gameObject.transform.localScale.y / 2;

        //WallZ draged anywhere
        if (!wallCollided)
        {
            Debug.Log("WallZ is outside floor");
            gameObject.transform.position = new Vector3(mousePositionScript.mousePoition.x + offset.x, wallZHeight, mousePositionScript.mousePoition.z);
        }
        //WallZ collided with floor
        else if (floor != null && wallCollided)
        {
            float floorSizeX = floor.transform.localScale.x;
            float floorPositionX = floor.transform.position.x;
            float rightFloorBound = floorPositionX - (floorSizeX / 2) + (transform.localScale.x / 2);
            float leftFloorBound = floorPositionX + (floorSizeX / 2) - (transform.localScale.x / 2);
            Debug.Log("Right: " + rightFloorBound);                                                                     
            Debug.Log("Left: " + leftFloorBound);

            //WallZ hits right side of floor edge
            if (gameObject.transform.position.x < rightFloorBound)
            {
                Debug.Log("WallZ hits right floor edge");
                Debug.Log("Position: " + gameObject.transform.position);
                gameObject.transform.position = new Vector3(rightFloorBound, wallZHeight, transform.position.z);
            }

            //WallZ hits left side of floor edge
            else if (gameObject.transform.position.x > leftFloorBound)
            {
                Debug.Log("WallZ hits left floor edge");
                Debug.Log("Position: " + gameObject.transform.position);
                gameObject.transform.position = new Vector3(leftFloorBound, wallZHeight, transform.position.z);
            }

            //WallZ in the middle of floor edge
            else if (gameObject.transform.position.x <= leftFloorBound && gameObject.transform.position.x >= rightFloorBound)
            {

                mousePositionScript.mousePoition.x = Mathf.Clamp(mousePositionScript.mousePoition.x, rightFloorBound, leftFloorBound);
                Debug.Log("WallZ in the middle of floor edge");
                Debug.Log("Position: " + gameObject.transform.position);
                gameObject.transform.position = new Vector3(mousePositionScript.mousePoition.x, wallZHeight, transform.position.z);
            }

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //WallZ collide with floor
        if (other.transform.CompareTag("Floor"))
        {

            Debug.Log("Collide with floor");
            wallCollided = true;
            floor = other.gameObject;
            float floorSizeZ = floor.transform.localScale.z;
            float floorPositionZ = floor.transform.position.z;
            float backFloorBound = floorPositionZ - floorSizeZ / 2 + 0.125f;
            float frontFloorBound = floorPositionZ + floorSizeZ / 2 - 0.125f;

            //Collides with floor from behind
            
            if (gameObject.transform.position.z < floorPositionZ)
            {
                Debug.Log("Uderza z tylu");
                mousePositionScript.mousePoition.z = backFloorBound;
                position = new Vector3(transform.position.x, y / 2, backFloorBound);
                gameObject.transform.position = position;
            }
            //Collides with floor from front
            else if (gameObject.transform.position.z > frontFloorBound)
            {
                Debug.Log("Uderza z przodu");
                mousePositionScript.mousePoition.z = frontFloorBound;
                position = new Vector3(transform.position.x, y / 2, frontFloorBound);
                gameObject.transform.position = position;
            }
        }
    }

}
