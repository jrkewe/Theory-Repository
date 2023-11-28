using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class MousePosition : MonoBehaviour
{
    //mouse position in 3D
    public Camera mainCamera;
    public Vector3 mousePoition;

    //selected object
    public GameObject selectedObject;
    public int objectID;

    private void Update()
    {
        GetMousePosition();
    }

    public bool DetectObject()
    {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit raycastHit;
            if (Physics.Raycast(ray, out raycastHit))
            {
                
                if (raycastHit.collider.gameObject.tag == "WallX")
                {
                selectedObject = raycastHit.collider.gameObject;
                objectID = 0;
                Debug.Log("Wall X");
                Debug.Log(selectedObject.transform.localScale);
                Debug.Log("Position: " +selectedObject.transform.position);
                return true;
                }
                else if (raycastHit.collider.gameObject.tag == "WallZ")
                {
                selectedObject = raycastHit.collider.gameObject;
                objectID = 1;
                Debug.Log("Wall Z");
                Debug.Log(selectedObject.transform.localScale);
                return true;
                }
                else if (raycastHit.collider.gameObject.tag == "Floor")
                {
                selectedObject = raycastHit.collider.gameObject;
                objectID = 2;
                Debug.Log("Floor");
                Debug.Log(selectedObject.transform.localScale);
                return true;
                }
                else
                {
                    return false;
                }
            }
            else 
            {
                return false;
            }
    }

    public GameObject GetObject() 
    {
        return selectedObject;
    }

    public int GetID()
    {
        return objectID;
    }

    public void GetMousePosition() 
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit))
        {
            transform.position = raycastHit.point;
            mousePoition = transform.position;
            mousePoition.y = 0.0f;
        }
    }
  
}
