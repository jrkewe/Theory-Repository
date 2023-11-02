using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;
using UnityEngine.UI;

public class Buttons : MonoBehaviour
{
    private Button button;
    public MainManager mainManager;
    public int buttonNumber;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        mainManager = GameObject.Find("Main Manager").GetComponent<MainManager>();
        button.onClick.AddListener(LoadObject);
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void LoadObject()
    {
        mainManager.CreateObject(buttonNumber);
    }
}
