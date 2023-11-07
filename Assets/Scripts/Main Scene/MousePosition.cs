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

    //one mouse click happend
    public bool objectIsChoosen;
    public bool otherObjectIsChoosen;
    public bool setBooleanState = true;

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
                Debug.Log("Wall X");
                return true;
                }
                else if (raycastHit.collider.gameObject.tag == "WallZ")
                {
                selectedObject = raycastHit.collider.gameObject;
                //Debug.Log("Wall Z");
                    return true;
                }
                else if (raycastHit.collider.gameObject.tag == "Floor")
                {
                selectedObject = raycastHit.collider.gameObject;
                //Debug.Log("Floor");
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else 
            {//
                return false;
            }
    }

    public GameObject GetObject() 
    {
        //Debug.Log(selectedObject.gameObject.name+ "Selected Object name");
        return selectedObject;
    }

    public void GetMousePosition() 
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit))
        {
            transform.position = raycastHit.point;
            mousePoition = transform.position;
        }
    }
  
}
