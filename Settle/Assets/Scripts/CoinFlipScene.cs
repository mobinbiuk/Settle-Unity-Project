using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinFlipScene : MonoBehaviour
{
    public GameObject oathPopUpNotif;
    public GameObject touchIds;
    public RawImage touchIdTop;
    public RawImage touchIdBot;
    private bool playerTopTouched;
    private bool playerBotTouched;
    private bool isCheckingForTouches;
    public GameObject playersTouchError;
    public GameObject countDownParent;
    public Text countDownText;
    private float countDownTimer = 3.0f;
    private bool coinFlipGameRunning = false;


   
    private void Start()
    {
        StartCoroutine(ShowOathPopUpAfterDelay(0.5f));
    }

    IEnumerator ShowOathPopUpAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        oathPopUpNotif.SetActive(true);

        yield return new WaitForSeconds(5f);
        oathPopUpNotif.SetActive(false);

        yield return new WaitForSeconds(1f);
        touchIds.SetActive(true);
        StartCoroutine(EnableTouchInputAfterDelay(0f));
    }
    IEnumerator EnableTouchInputAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        isCheckingForTouches = true;
        Debug.Log("EnableTouchInputAfterDelay");
    }

    private void Update()
    {
        if (isCheckingForTouches)
        {
            CheckTouchInput();
        }
        if (coinFlipGameRunning)
        {
            Invoke("StartCountDown",0f);
        }
    }

    void CheckTouchInput()
    {
        for (int i = 0; i < Input.touchCount; i++)
        {
            Touch touch = Input.GetTouch(i);
            Debug.Log($"Touch {i + 1}: Position - {touch.position}, Phase - {touch.phase}");
            CheckTouchPosition(touch.position);
        }
    }

    void CheckTouchPosition(Vector2 touchPosition)
    {
        bool topTouch = RectTransformUtility.RectangleContainsScreenPoint(touchIdTop.rectTransform, touchPosition);
        bool botTouch = RectTransformUtility.RectangleContainsScreenPoint(touchIdBot.rectTransform, touchPosition);

        if (topTouch)
        {
            playerTopTouched = true;
            Debug.Log("Player top Touched!");
        }

        if (botTouch)
        {
            playerBotTouched = true;
            Debug.Log("Player bot Touched!");
        }

        // Check if either player touched
        if (playerTopTouched || playerBotTouched)
        {
            StartCoroutine(CheckPlayersTouchedCoroutine());
        }
    }

    IEnumerator CheckPlayersTouchedCoroutine()
    {
        yield return new WaitForSeconds(3f);

        if (!playerTopTouched || !playerBotTouched)
        {
            StartCoroutine(ShowPlayersTouchErrorAndReset(0f));
        }
        else
        {
            // Both players touched, start the game
            StartCoroutine(StartCoinFlipGame());
        }
    }

    IEnumerator StartCoinFlipGame()
    {
        Debug.Log("Starting CoinFlipGame coroutine...");

        yield return new WaitForSeconds(0.5f);
        isCheckingForTouches = false;
        touchIds.SetActive(false);
        coinFlipGameRunning = true;
        
    }

    IEnumerator ShowPlayersTouchErrorAndReset(float delay=0.1f)
    {
        yield return new WaitForSeconds(delay);
        playersTouchError.SetActive(true);
        Debug.Log("ShowPlayersTouchErrorAndResets");

        yield return new WaitForSeconds(3f);

        // Reset touch states
        playersTouchError.SetActive(false);

        // Restart the touch input checking process
        StartCoroutine(CheckPlayersTouchedCoroutine());
    }
    void StartCountDown()
    {
        countDownParent.SetActive(true);
        countDownTimer -= Time.deltaTime;
        countDownText.text = (countDownTimer).ToString("0");
        if (countDownTimer <= 1)
        {
            countDownText.text = (1).ToString("0");
            Invoke("CountDownEnded", 0.5f);

        }
        
    }
    
    void CountDownEnded()
    {
        coinFlipGameRunning = false;
        countDownParent.SetActive(false);
    }

}

