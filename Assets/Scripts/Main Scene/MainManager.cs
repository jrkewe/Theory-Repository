using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public GameObject[] objectsPrefabs;

    private void Start()
    {
    }
    private void Update()
    {
        //if (Input.GetMouseButtonDown(0)) 
        //{
        //    Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //    objectsPrefabs[buttonNumber]
        //}
    }

    public void CreateObject(int buttonNumber)
    {
        Instantiate(objectsPrefabs[buttonNumber], new Vector3(1100,0 ,0), transform.rotation);
        objectsPrefabs[buttonNumber].GetComponent<Renderer>().enabled = false;
    }

}
