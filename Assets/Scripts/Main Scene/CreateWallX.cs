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
        //issue
        if (!wallCollided)
        {
            transform.position = mousePositionScript.mousePoition + offset;
            SetPosition();
        }
        else if (floor!=null && wallCollided)
        {
            float floorSizeZ = floor.transform.localScale.z;
            float floorPositionZ = floor.transform.position.z;
            if (transform.position.z > (floorPositionZ - (floorSizeZ / 2) + (transform.localScale.z / 2)) &&
                transform.position.z < (floorPositionZ + (floorSizeZ / 2) - (transform.localScale.z / 2)))
            {
                if (transform.position.z >= (floorPositionZ - (floorSizeZ / 2) + (transform.localScale.z / 2)) &&
                    transform.position.z <= (floorPositionZ + (floorSizeZ / 2) - (transform.localScale.z / 2)))
                {
                    transform.position = new Vector3(transform.position.x, y / 2, mousePositionScript.mousePoition.z + offset.z);
                }
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Floor"))
        {
            wallCollided = true;
            floor = other.gameObject;
            float floorSizeX = floor.transform.localScale.x;
            float floorPositionX = floor.transform.position.x;
            if (gameObject.transform.position.x < floor.transform.position.x)
            {
                mousePositionScript.mousePoition.x = floorPositionX - floorSizeX / 2 + 0.125f;
                position = new Vector3(floorPositionX - (floorSizeX / 2) + 0.125f, y / 2, transform.position.z);
                gameObject.transform.position = position;
            }
            else if (gameObject.transform.position.x > other.transform.position.x)
            {
                mousePositionScript.mousePoition.x = floorPositionX + floorSizeX / 2 - 0.125f;
                position = new Vector3(floorPositionX + (floorSizeX / 2) - 0.125f, y / 2, transform.position.z);
                gameObject.transform.position = position;
            }
        }
    }

}

