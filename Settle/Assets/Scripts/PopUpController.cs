using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupController : MonoBehaviour
{
    public GameObject popupPanel; // پنل Popup
    public float delayToShow = 1f; // تاخیر نمایش Popup
    public float displayTime = 10f; // زمان نمایش Popup در ثانیه

    private void Start()
    {
        // فراخوانی تابع ShowPopup با تاخیر
        Invoke("ShowPopup", delayToShow);
    }

    private void ShowPopup()
    {
        // نمایش پنل Popup
        popupPanel.SetActive(true);

        // فراخوانی تابع HidePopup با تاخیر بر اساس زمان نمایش
        Invoke("HidePopup", displayTime);
    }

    private void HidePopup()
    {
        // مخفی کردن پنل Popup
        popupPanel.SetActive(false);
    }
}

