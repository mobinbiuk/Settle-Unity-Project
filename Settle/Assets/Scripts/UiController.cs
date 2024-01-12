using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiController : MonoBehaviour
{

    void Start()
    {
        // ExitButton
        Button exitButton = GetComponent<Button>();
        if (exitButton != null)
        {
            exitButton.onClick.AddListener(ExitGame);
        }
        else
        {
            Debug.LogError("ExitButton Script is not attached to a Button!");
        }
    }

    void ExitGame()
    {
        //Exiting unity
        if (Application.isPlaying)
        {
            UnityEditor.EditorApplication.isPlaying = false;
            Application.Quit();
        }

        

    }
}
    

