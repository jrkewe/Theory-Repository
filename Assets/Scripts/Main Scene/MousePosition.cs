using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class MousePosition : MonoBehaviour
{
    public Camera mainCamera;
    public Vector3 mousePoition;

    private void Update() 
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit)) 
        {
            transform.position = raycastHit.point;
            mousePoition = transform.position;
        }

    }
}

