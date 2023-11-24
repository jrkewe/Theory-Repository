using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static UnityEditor.ShaderData;
using System.Globalization;
using System;
using Unity.VisualScripting;
using UnityEngine.UIElements;

public class ReadInput : MonoBehaviour
{

    public TMP_InputField inputFieldX;
    public TMP_InputField inputFieldY;
    public TMP_InputField inputFieldZ;

    private UserInputManager userInputManagerScript;

    private void Start()
    {
        inputFieldX.text = "Enter X dimension";
        inputFieldY.text = "Enter Y dimension";
        inputFieldZ.text = "Enter Z dimension";

        inputFieldX.DeactivateInputField();
        inputFieldY.DeactivateInputField();
        inputFieldZ.DeactivateInputField();

        userInputManagerScript = GameObject.Find("User Input Manager").GetComponent<UserInputManager> ();
    }

    private void Update()
    {
        if (userInputManagerScript.wallWasClicked)
        {
            Debug.Log("Wlacz input");
            StartCoroutine(WaitForEnter());
        }
    }

    IEnumerator WaitForEnter()
    {
        inputFieldX.ActivateInputField();
        inputFieldY.ActivateInputField();
        inputFieldZ.ActivateInputField();

        //wprowadzaj wymiary
        while (!Input.GetKeyDown(KeyCode.Return))
        {
            yield return null;
        }
        //przeslij wymiary 
        Debug.Log("Text: " +inputFieldX.text);
        float x = float.Parse(inputFieldX.text, NumberStyles.Any, new CultureInfo("en-US"));

        // float y = float.Parse(inputFieldY.text);
        // float z = float.Parse(inputFieldZ.text);
        userInputManagerScript.newSize = new Vector3(5.0f,5.0f,5.0f);

        inputFieldX.text = "Enter X dimension";
        inputFieldY.text = "Enter Y dimension";
        inputFieldZ.text = "Enter Z dimension";

        inputFieldX.DeactivateInputField();
        inputFieldY.DeactivateInputField();
        inputFieldZ.DeactivateInputField();
    }
}
