using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateFloor : Prefabricate
{
    private MousePosition mousePositionScript;

    private void Start()
    {
        objectId = 2;
        SetDimensions();
        SetPosition();
        mousePositionScript = GameObject.Find("User Input Manager").GetComponent<MousePosition>();
    }


    public override void SetDimensions()
    {
        gameObject.transform.localScale = new Vector3(x, 0.16f, z);
    }

    public override void SetPosition()
    {
        transform.position = new Vector3(transform.position.x, 0.08f, transform.position.z);
    }

    private void OnMouseDrag()
    {
        transform.position = mousePositionScript.mousePoition;
        transform.position = new Vector3(transform.position.x, 0.08f, transform.position.z);
    }

}
