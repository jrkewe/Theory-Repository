using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;
using UnityEngine.UI;

public class Buttons : MonoBehaviour
{
    private Button button;
    public MainManager mainManager;

    //tells me with type of object it is
    public int buttonNumber;
    
    public bool buttonIsClicked = false;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        mainManager = GameObject.Find("Main Manager").GetComponent<MainManager>();
        button.onClick.AddListener(LoadObject);
    }



    public void LoadObject()
    {
        buttonIsClicked = true;
        mainManager.ObjectWasChoosen(buttonIsClicked);
        mainManager.ButtonNumber(buttonNumber);
    }
}
