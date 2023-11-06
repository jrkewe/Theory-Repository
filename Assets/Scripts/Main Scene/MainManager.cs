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
                mousePositionScript.GetObject();
                StartCoroutine(WaitForMouseClick());
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
    IEnumerator WaitForMouseClick()
    {
        while (!Input.GetKeyDown(KeyCode.Delete))
        { 
            yield return null;
        }
        Destroy(mousePositionScript.GetObject());
    }

 

}
