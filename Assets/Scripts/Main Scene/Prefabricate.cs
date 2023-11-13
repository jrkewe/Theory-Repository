using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;

public class Prefabricate : MonoBehaviour
{
    protected float x = 1.0f;
    protected float y = 1.0f;
    protected float z = 1.0f;

    protected int objectId;

    public virtual void SetDimensions()
    {
        gameObject.transform.localScale = new Vector3(x, y, z);
    }

    public virtual void SetPosition()
    {
        gameObject.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }


}

