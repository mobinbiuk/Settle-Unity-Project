using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CoinFlipScene : MonoBehaviour
{
    public Button exitButton;
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

    //implementation of the game 
    public GameObject simpleCoins;
    public RawImage winnerCoin;
    public RawImage loserCoin;
    private Transform chosenCoinTransform;
    private bool hasPlayerChosen = false;
    private bool implementCoinFlipRunning = false;
    public Canvas canvas;


    private void Start()
    {
        if (exitButton != null)
        {
            exitButton.onClick.AddListener(OnExitButtonClick);
        }
        else
        {
            Debug.Log("Exit Button Not Found");
        } 

        StartCoroutine(ShowOathPopUpAfterDelay(0.5f));
    }

    private void Update()
    {
        if (isCheckingForTouches)
        {
            CheckTouchInput();
        }
        if (coinFlipGameRunning)
        {
            Invoke("StartCountDown", 0f);
        }
        if (implementCoinFlipRunning && !hasPlayerChosen && Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                // Convert touch position to UI space
                Vector2 touchPosition = touch.position;
                RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, touchPosition, Camera.main, out Vector2 localPoint);

                // Check if the touch position is within any of the simple coins' RectTransform
                RectTransform[] simpleCoinRectTransforms = simpleCoins.GetComponentsInChildren<RectTransform>();
                foreach (RectTransform rectTransform in simpleCoinRectTransforms)
                {
                    if (RectTransformUtility.RectangleContainsScreenPoint(rectTransform, touch.position, Camera.main))
                    {
                        // Simple coin touched, proceed with the game logic
                        ImplementCoinFlipGame(rectTransform.position);
                        hasPlayerChosen = true;
                        Debug.Log("ImplementCoinFlipGame called.");
                        break; // Exit the loop once a coin is found
                    }
                }
            }
        }
    }

    public void OnExitButtonClick()
    {
        Application.Quit();
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif

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
        implementCoinFlipRunning = true;
        simpleCoins.SetActive(true);

    }

    //implement logic of choosing coins
    void ImplementCoinFlipGame(Vector2 spawnPoint)
    {
        Debug.Log("Implementcoinflipgame");
        
        RawImage coinRawImage = Random.Range(0, 2) == 0 ? winnerCoin : loserCoin;
        RawImage newCoin = Instantiate(coinRawImage);
        // Set the parent to the canvas
        newCoin.rectTransform.SetParent(canvas.transform, false);
        // Position relative to the canvas
        newCoin.rectTransform.anchoredPosition = spawnPoint;
        implementCoinFlipRunning = false;
    }


    //If both of players dont touch the touchId shows up
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

    //starts CountDown from 3 to 1
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
    
    //Ends and disables CountDown
    void CountDownEnded()
    {
        coinFlipGameRunning = false;
        countDownParent.SetActive(false);
    }

}

