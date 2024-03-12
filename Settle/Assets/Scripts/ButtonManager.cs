using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NewBehaviourScript : MonoBehaviour
{
    void Start()
    {
        Button coinFlipButton = GameObject.Find("CoinFlipButton").GetComponent<Button>();
        Button exitButton = GameObject.Find("ExitButton").GetComponent<Button>();

        if (coinFlipButton != null)
        {
            coinFlipButton.onClick.AddListener(OnCoinFlipButtonClick);
        }
        else
        {
            Debug.Log("CoinFlipButton not found");
        }

        
        if (exitButton != null)
        {
            exitButton.onClick.AddListener(OnExitButtonClick);
        }
        else
        {
            Debug.Log("Exit Button Not Found");
        }

    }

   
    public void OnCoinFlipButtonClick()
    {
        if (SceneManager.GetSceneByName("CoinFlipScene")!=null)
        {
            SceneManager.LoadScene("CoinFlipScene");
        }
        else
        {
            Debug.Log("CoinFlipScene not found");
        }
    }

    public void OnExitButtonClick()
    {
        Application.Quit();
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif

    }
}
