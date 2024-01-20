using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupController : MonoBehaviour
{
    [SerializeField] private GameObject popupPanel;
    private GameObject touchId;
    
    private float delayToShow = 1f; 
    private float displayTime = 5f; 

    private void Start()
    {
        touchId = GameObject.FindWithTag("TouchIdParent");
        Invoke("ShowPopup", delayToShow);
    }

    private void ShowPopup()
    {
        popupPanel.SetActive(true);
        Invoke("HidePopup", displayTime);
    }

    private void HidePopup()
    {
        popupPanel.SetActive(false);
        Invoke("ShowTouchId", delayToShow);
    }
    private void ShowTouchId()
    {
        Debug.Log("show touchid");
        touchId.SetActive(true);
    }
}

