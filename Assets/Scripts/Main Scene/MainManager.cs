using System.Collections;
using System.Collections.Generic;
using UnityEditor.Sprites;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public GameObject[] objectsPrefabs;
    public int prefabIndex;
    public bool buttonWasClicked = false;

    //mouse position
    public MousePosition mousePositionScript;

    int key = -1;



    public void Start()
    {
        mousePositionScript = GetComponent<MousePosition>();
    }




    private void Update()
    {
        InstantiateObjects();
    }




    public void ObjectWasChoosen(bool buttonIsClicked)
    {
        buttonWasClicked = buttonIsClicked;
    }

    public void ButtonNumber(int buttonNumber)
    { 
        prefabIndex = buttonNumber;
    }

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

}
