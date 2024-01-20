using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpManager : MonoBehaviour
{
    [SerializeField] private GameObject _popupPanel;
    [SerializeField] private float displayTimePopup;
    [SerializeField] private float delayToShowPopup;
    private Image _touchIdTop;
    private Image _touchIdBot;
    void Start()
    {
        _touchIdTop = GameObject.Find("TouchIdTop").GetComponent<Image>();
        _touchIdBot = GameObject.Find("TouchIdBottom").GetComponent<Image>();

        Invoke("ShowPopup", delayToShowPopup);
    }
    public void ShowPopup()
    { 
        _popupPanel.SetActive(true);
        Invoke("HidePopup", displayTimePopup);
    }
    public void HidePopup()
    {
        _popupPanel.SetActive(false);
        Invoke("TouchIdAppear", 0.5f);

    }
    public void TouchIdAppear()
    {
        if (_touchIdTop != null)
        {
            _touchIdTop.enabled = true;
            _touchIdBot.enabled = true;
        }

    }

}
