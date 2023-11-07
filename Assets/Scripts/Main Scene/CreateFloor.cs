using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateFloor : Prefabricate
{
    
    private void Start()
    {
        SetDimensions();
        SetPosition();
        SetObjectID();
        Debug.Log(objectId ,gameObject);
    }

    public override void SetDimensions()
    {
        // base.SetDimensions();
        gameObject.transform.localScale = new Vector3(x, 0.25f, z);
    }

    public override void SetPosition()
    {
        gameObject.transform.position = new Vector3(transform.position.x, 0.15f, transform.position.z);
    }

    public override void SetObjectID()
    {
        objectId = 2;
    }
}
