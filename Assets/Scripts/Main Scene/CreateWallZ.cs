using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateWallZ : Prefabricate
{

    private void Start()
    {
        objectId = 1;
        SetDimensions();
        SetPosition();
    }

    public override void SetDimensions()
    {
        gameObject.transform.localScale = new Vector3(x, y, 0.25f);
    }

    public override void SetPosition()
    {
        gameObject.transform.position = new Vector3(transform.position.x, y/2, transform.position.z);
    }

}
