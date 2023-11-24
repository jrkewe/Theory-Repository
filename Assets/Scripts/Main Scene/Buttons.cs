using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;
using UnityEngine.UI;

public class Buttons : MonoBehaviour
{
    //Buttons
    private Button button;
    private UserInputManager userInputManager;

    //tells me with type of object it is
    public int buttonNumber;
    
    private bool buttonIsClicked = false;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        userInputManager = GameObject.Find("User Input Manager").GetComponent<UserInputManager>();
        button.onClick.AddListener(LoadObject);
    }



    public void LoadObject()
    {
        buttonIsClicked = true;
        userInputManager.ObjectWasChoosen(buttonIsClicked);
        userInputManager.ButtonNumber(buttonNumber);
    }
}
