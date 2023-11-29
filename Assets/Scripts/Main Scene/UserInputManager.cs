using System.Collections;
using System.Collections.Generic;
using UnityEditor.Sprites;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UserInputManager : MonoBehaviour
{
    
    //Prefabs objects
    public GameObject[] objectsPrefabs;

    //Buttons Script
    private bool buttonWasClicked = false;
    private int prefabIndex;
    
    //Mouse position
    private MousePosition mousePositionScript;

    //Resizing
    public bool wallWasClicked= false;
    public Vector3 newSize;

    public void Start()
    {
        mousePositionScript = GetComponent<MousePosition>();
    }


    private void Update()
    {
        
        if (Input.GetMouseButtonDown(0)) 
        {
            wallWasClicked = false;
            StopAllCoroutines();

            //if its object - DetectObject
            if (mousePositionScript.DetectObject())
            {
                wallWasClicked = true;
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
        //Debug.Log("New size: " +newSize);

        while (!Input.GetKeyDown(KeyCode.D))
        {
            yield return null;
        }
        if (newSize!=Vector3.zero) {
            if (mousePositionScript.GetID() == 0)
            {
                float key = newSize.x;
                newSize.x = 0.25f;
                mousePositionScript.GetObject().transform.localScale = newSize;
                mousePositionScript.GetObject().transform.position = new Vector3(mousePositionScript.GetObject().transform.position.x, newSize.y / 2, mousePositionScript.GetObject().transform.position.z);
                newSize.x = key;
            }
            else if (mousePositionScript.GetID() == 1)
            {
                float key = newSize.z;
                newSize.z = 0.25f;
                mousePositionScript.GetObject().transform.localScale = newSize;
                mousePositionScript.GetObject().transform.position = new Vector3(mousePositionScript.GetObject().transform.position.x, newSize.y / 2, mousePositionScript.GetObject().transform.position.z);
                newSize.z = key;
            }
            else if (mousePositionScript.GetID() == 2)
            {
                float key = newSize.y;
                newSize.y = 0.16f;
                mousePositionScript.GetObject().transform.localScale = newSize;
                mousePositionScript.GetObject().transform.position = new Vector3(mousePositionScript.GetObject().transform.position.x, 0.08f, mousePositionScript.GetObject().transform.position.z);
                newSize.y = key;
            }
        }
        else 
        {
            Debug.Log("Enter size again");
        }
    }

}
