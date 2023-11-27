using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObject : MonoBehaviour
{
    private MousePosition mousePositionScript;

    private void Start()
    {
        mousePositionScript = GameObject.Find("User Input Manager").GetComponent<MousePosition>();
    }

    private void OnMouseDrag()
    {
        transform.position = mousePositionScript.mousePoition;
    }

}
