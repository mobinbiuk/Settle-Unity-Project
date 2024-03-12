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
            StartCoroutine(StartCoinFlipGame(0f));
        }
    }

    IEnumerator StartCoinFlipGame(float delay)
    {
        Debug.Log("Starting CoinFlipGame coroutine...");
        // Wait for a short delay before starting the coinflip game
        yield return new WaitForSeconds(delay);

        // Implement your logic to start the coinflip game
        Debug.Log("Both players touched! Starting the coinflip game...");

        // Stop checking for touches
        isCheckingForTouches = false;

        // Disable touch ids
        touchIds.SetActive(false);
    }

    IEnumerator ShowPlayersTouchErrorAndReset(float delay)
    {
        yield return new WaitForSeconds(0.1f);
        playersTouchError.SetActive(true);
        Debug.Log("ShowPlayersTouchErrorAndResets");

        yield return new WaitForSeconds(3f);

        // Reset touch states
        playersTouchError.SetActive(false);

        // Restart the touch input checking process
        StartCoroutine(CheckPlayersTouchedCoroutine());
    }
}
