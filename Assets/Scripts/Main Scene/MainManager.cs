using System.Collections;
using System.Collections.Generic;
using UnityEditor.Sprites;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    
    //Prefabs objects
    public GameObject[] objectsPrefabs;

    //Buttons Script
    public bool buttonWasClicked = false;
    public int prefabIndex;

    //Mouse position
    public MousePosition mousePositionScript;

    public float X ;
    public float Y ;
    public float Z ;


    public void Start()
    {
        mousePositionScript = GetComponent<MousePosition>();
    }


    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        { 
            StopAllCoroutines();

            //if its object - DetectObject
            if (mousePositionScript.DetectObject())
            {
                StartCoroutine(WaitForResize());
                StartCoroutine(WaitForDelete());
            }
            
            //if its terrain - InstantiateObject
            else
            {
                InstantiateObjects();
            }
        }
    }

    //Buttons Script Values
    public void ObjectWasChoosen(bool buttonIsClicked)
    {
        buttonWasClicked = buttonIsClicked;
    }
    public void ButtonNumber(int buttonNumber)
    { 
        prefabIndex = buttonNumber;
    }


    //Create Prefabs
    public void InstantiateObjects() 
    {
        if (Input.GetMouseButtonDown(0) && buttonWasClicked)
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }
            else
            {
                Instantiate(objectsPrefabs[prefabIndex], mousePositionScript.mousePoition, transform.rotation);
            }

        }
    }

    //Wait till Delete is pressed
    IEnumerator WaitForDelete()
    {
        while (!Input.GetKeyDown(KeyCode.Delete))
        { 
            yield return null;
        }
        Destroy(mousePositionScript.GetObject());
    }

    //Wait till change size 
    IEnumerator WaitForResize()
    {
        //input
        X = 5.0f;
        Y = 5.0f;
        Z = 5.0f;

        while (!Input.GetKeyDown(KeyCode.D))
        {
            yield return null;
        }

        if (mousePositionScript.GetID() == 0)
        {
            mousePositionScript.GetObject().transform.localScale = new Vector3(0.25f,Y, Z);
            mousePositionScript.GetObject().transform.position = new Vector3(mousePositionScript.GetObject().transform.position.x, Y/ 2, mousePositionScript.GetObject().transform.position.z);
        }
        else if (mousePositionScript.GetID() == 1)
        {
            mousePositionScript.GetObject().transform.localScale = new Vector3(X, Y, 0.25f);
            mousePositionScript.GetObject().transform.position = new Vector3(mousePositionScript.GetObject().transform.position.x, Y / 2, mousePositionScript.GetObject().transform.position.z);
        }
        else if (mousePositionScript.GetID() == 2)
        {
            mousePositionScript.GetObject().transform.localScale = new Vector3(X, 0.16f, Z);
            mousePositionScript.GetObject().transform.position = new Vector3(mousePositionScript.GetObject().transform.position.x, 0.08f, mousePositionScript.GetObject().transform.position.z);
        }
    }

}
