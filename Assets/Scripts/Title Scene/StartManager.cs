using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartManager : MonoBehaviour
{
    public Button button;

    // Start is called before the first frame update
    void Start()
    {
        button =GetComponent<Button>();
        button.onClick.AddListener(LoadNextScene);
    }

    void LoadNextScene() 
    {
        SceneManager.LoadScene(1); 
    }
}
