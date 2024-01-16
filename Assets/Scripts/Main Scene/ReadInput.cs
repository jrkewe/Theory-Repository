using System.Collections;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ReadInput : MonoBehaviour
{
    //Debuger
    public static ILogger debugReadInput = new Logger(Debug.unityLogger.logHandler);

    public TMP_InputField inputFieldX;
    public TMP_InputField inputFieldY;
    public TMP_InputField inputFieldZ;

    private UserInputManager userInputManagerScript;
    private MousePosition mousePositionScript;

    //New Dimensions script
    private NewDimensions newDimensionsScript;

    private void Start()
    {
        //Debugger
        debugReadInput.logEnabled = true;

        RestartInputFields();

        DisabelAllInputFields();

        userInputManagerScript = GameObject.Find("User Input Manager").GetComponent<UserInputManager> ();
        mousePositionScript = GameObject.Find("User Input Manager").GetComponent<MousePosition>();
        newDimensionsScript = GameObject.Find("SetDimensions").GetComponent<NewDimensions>();
    }

    private void Update()
    {

        newDimensionsScript.enterWasClicked = false;

        if (userInputManagerScript.objectDetected && mousePositionScript.selectedObject != null && !mousePositionScript.mouseDragsObject)
        {
            DisplayDimensionsOfSelectedObject();

            EnableTwoInputFields();

            StartCoroutine(WaitForEnter());
        }
    }

    IEnumerator WaitForEnter()
    {

        //wprowadzaj wymiary
        while (!Input.GetKey(KeyCode.Return) )
        {
            if (Input.GetMouseButton(0) && (mousePositionScript.DetectObject() || mousePositionScript.terrainHItted))
            {
                RestartInputFields();
                yield return null;
                StopCoroutine(WaitForEnter());
            }
            yield return null;
        }
        newDimensionsScript.enterWasClicked = true;

        //przeslij wymiary 
        if (mousePositionScript.objectID == 0)
        {
            float x = 0.0f;
            float y = float.Parse(inputFieldY.text);
            float z = float.Parse(inputFieldZ.text);
            userInputManagerScript.newSize = new Vector3(x, y, z);
        }
        else if (mousePositionScript.objectID == 1)
        {
            float x = float.Parse(inputFieldX.text);
            float y = float.Parse(inputFieldY.text);
            float z = 0.0f;
            userInputManagerScript.newSize = new Vector3(x, y, z);
        }
        else if (mousePositionScript.objectID == 2)
        {

            float x = float.Parse(inputFieldX.text);
            float y = 0.0f;
            float z = float.Parse(inputFieldZ.text);
            userInputManagerScript.newSize = new Vector3(x, y, z);
        }


        RestartInputFields();
        DisabelAllInputFields();
    }

    private void EnableTwoInputFields() 
    {
        if (mousePositionScript.objectID == 0)
        {
            inputFieldY.enabled = true;
            inputFieldZ.enabled = true;
        }
        else if (mousePositionScript.objectID == 1)
        {
            inputFieldX.enabled = true;
            inputFieldY.enabled = true;
        }
        else if (mousePositionScript.objectID == 2)
        {
         
            inputFieldX.enabled = true;
            inputFieldZ.enabled = true;
        }
    }

    private void DisabelAllInputFields()
    {
        inputFieldX.enabled = false;
        inputFieldY.enabled = false;
        inputFieldZ.enabled = false;
    }

    private void RestartInputFields()
    {
        inputFieldX.text = "Enter X dimension";
        inputFieldY.text = "Enter Y dimension";
        inputFieldZ.text = "Enter Z dimension";
        inputFieldX.image.color = Color.white;
        inputFieldY.image.color = Color.white;
        inputFieldZ.image.color = Color.white;
    }

    private void DisplayDimensionsOfSelectedObject()
    {
        if (mousePositionScript.objectID == 0) //WallX
        {
            inputFieldX.image.color = Color.red;
            inputFieldX.text = "Fixed dimension";
            inputFieldY.text = mousePositionScript.selectedObject.transform.localScale.y.ToString();
            inputFieldZ.text = mousePositionScript.selectedObject.transform.localScale.z.ToString();
        }
        if (mousePositionScript.objectID == 1) //WallZ
        {
            inputFieldX.text = mousePositionScript.selectedObject.transform.localScale.x.ToString();
            inputFieldY.text = mousePositionScript.selectedObject.transform.localScale.y.ToString();
            inputFieldZ.image.color = Color.red;
            inputFieldZ.text = "Fixed dimension";
        }
        if (mousePositionScript.objectID == 2) //Floor Y
        {
            inputFieldX.text = mousePositionScript.selectedObject.transform.localScale.x.ToString();
            inputFieldY.image.color = Color.red;
            inputFieldY.text = "Fixed dimension";
            inputFieldZ.text = mousePositionScript.selectedObject.transform.localScale.z.ToString();
        }

    }

}
