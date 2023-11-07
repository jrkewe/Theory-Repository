using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateWallZ : Prefabricate
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
        gameObject.transform.localScale = new Vector3(x, y, 0.25f);
    }

    public override void SetPosition()
    {
        gameObject.transform.position = new Vector3(transform.position.x, y/2, transform.position.z);
    }

    public override void SetObjectID()
    {
        objectId = 1;
    }
}
