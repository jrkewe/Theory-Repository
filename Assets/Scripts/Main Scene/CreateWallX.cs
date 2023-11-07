using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;

public class CreateWallX : Prefabricate
{
    
    private void Start()
    {
        SetDimensions();
        SetPosition();
        SetObjectID();
        Debug.Log(objectId, gameObject);
    }

    public override void SetDimensions()
    {
        // base.SetDimensions();
        gameObject.transform.localScale = new Vector3(0.25f, y, z);
    }

    public override void SetPosition()
    {
        gameObject.transform.position = new Vector3(transform.position.x, y/2, transform.position.z);
    }

    public override void SetObjectID()
    {
        objectId = 0;
    }
}