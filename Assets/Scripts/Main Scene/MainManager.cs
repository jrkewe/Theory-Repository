using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public GameObject[] objectsPrefabs;
    public int prefabIndex;
    public bool buttonWasClicked = false;

    //mouse position
    public MousePosition mousePositionScript;

    int key = -1;



    private void Start()
    {
        mousePositionScript = GetComponent<MousePosition>();
    }




    private void Update()
    {
       //Debug.Log("prefabIndex: " + prefabIndex);
        //Debug.Log("key: " + key);
        if (Input.GetMouseButtonDown(0) && buttonWasClicked)
        {
            
            //first button click
            if (key == -1)
            {
                Debug.Log("if1");
                key = prefabIndex;
                Instantiate(objectsPrefabs[prefabIndex], mousePositionScript.mousePoition, transform.rotation);
                return;
            }
            //if we click on button
            else if (key != prefabIndex)
            {
                Debug.Log("if2");
                key = prefabIndex;
                return;
            }
            else if (key == prefabIndex)
            {
                Debug.Log("if3");
                Instantiate(objectsPrefabs[prefabIndex], mousePositionScript.mousePoition, transform.rotation);
            }
        }
    }




    public void ObjectWasChoosen(bool buttonIsClicked)
    {
        buttonWasClicked = buttonIsClicked;
    }

    public void ButtonNumber(int buttonNumber)
    { 
        prefabIndex = buttonNumber;
    }
}
