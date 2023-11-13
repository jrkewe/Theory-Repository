using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateWallX : Prefabricate
{

    private void Start()
    {
        objectId = 0;
        SetDimensions();
        SetPosition();
    }

    public override void SetDimensions()
    {
        transform.localScale = new Vector3(0.25f, y, z);
    }

    public override void SetPosition()
    {
        gameObject.transform.position = new Vector3(transform.position.x, y/2, transform.position.z);
    }

}

