using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.Rendering;
using System.Text;

public class ReadInput : MonoBehaviour
{

    public TMP_InputField inputFieldX;
    public TMP_InputField inputFieldY;
    public TMP_InputField inputFieldZ;

    private UserInputManager userInputManagerScript;
    private MousePosition mousePositionScript;

    private void Start()
    {
        inputFieldX.text = "Enter X dimension";
        inputFieldY.text = "Enter Y dimension";
        inputFieldZ.text = "Enter Z dimension";

        inputFieldX.DeactivateInputField();
        inputFieldY.DeactivateInputField();
        inputFieldZ.DeactivateInputField();

        userInputManagerScript = GameObject.Find("User Input Manager").GetComponent<UserInputManager> ();
        mousePositionScript = GameObject.Find("User Input Manager").GetComponent<MousePosition>();
    }

    private void Update()
    {
        if (userInputManagerScript.wallWasClicked && mousePositionScript.selectedObject!=null)
        {
            //Wpisz w input obecne wymiary sciany
            inputFieldX.text = mousePositionScript.selectedObject.transform.localScale.x.ToString();
            inputFieldY.text = mousePositionScript.selectedObject.transform.localScale.y.ToString();
            inputFieldZ.text = mousePositionScript.selectedObject.transform.localScale.z.ToString();

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

        float x = float.Parse(inputFieldX.text);
        float y = float.Parse(inputFieldY.text);
        float z = float.Parse(inputFieldZ.text);
       
        userInputManagerScript.newSize = new Vector3(x,y,z);

        inputFieldX.DeactivateInputField();
        inputFieldY.DeactivateInputField();
        inputFieldZ.DeactivateInputField();
    }
}
