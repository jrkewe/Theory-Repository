using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateWallZ : Prefabricate
{
    private MousePosition mousePositionScript;

    private void Start()
    {
        objectId = 1;
        SetDimensions();
        SetPosition();
        mousePositionScript = GameObject.Find("User Input Manager").GetComponent<MousePosition>();
    }

    public override void SetDimensions()
    {
        gameObject.transform.localScale = new Vector3(x, y, 0.25f);
    }

    public override void SetPosition()
    {
        gameObject.transform.position = new Vector3(transform.position.x, y/2, transform.position.z);
    }

    private void OnMouseDrag()
    {
        transform.position = mousePositionScript.mousePoition;
        transform.position = new Vector3(transform.position.x, y / 2, transform.position.z);
    }

}
