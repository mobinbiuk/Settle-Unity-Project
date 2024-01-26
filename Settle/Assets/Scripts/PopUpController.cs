using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PopupController : MonoBehaviour
{
    [SerializeField] private GameObject popupPanel;
    private GameObject touchId;

    private float delayToShow = 1f;
    private float displayTime = 5f;
    private float touchDuration = 3f;
    private bool isTouching = false;

    private void Start()
    {
        touchId = GameObject.FindWithTag("TouchIdParent");
        StartCoroutine(ShowPopup());
        
    }

    IEnumerator ShowPopup()
    {
        yield return new WaitForSeconds(delayToShow);
        popupPanel.SetActive(true);
        yield return new WaitForSeconds(displayTime);
        popupPanel.SetActive(false);
        

        StartCoroutine(CheckTouchDuration());
    }

    IEnumerator CheckTouchDuration()
    {
        Debug.Log("check touch duration");
        float startTime = Time.time;

        while (Time.time - startTime < touchDuration)
        {
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
            { 
                isTouching = false;
                yield break;
            }

            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                isTouching = true;
            }

            yield return null;
        }

        if (isTouching)
        { 
            SceneManager.LoadScene("CoinFlipScene");

        }
    }

    
}
