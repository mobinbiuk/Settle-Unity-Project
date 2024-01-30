using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CoinFlipController : MonoBehaviour
{
    private float timeLeft = 3.0f;
    private Text countDownText;
    private GameObject coinsParent;

    void Start()
    {
        countDownText = GetComponent<Text>();
        coinsParent = GameObject.FindWithTag("CoinsParent");
    }

   
    void Update()
    {
        Invoke("CountDownStart",0.5f);
    }
    void CountDownStart()
    {
        timeLeft -= Time.deltaTime;
        countDownText.text = (timeLeft).ToString("0");
        if (timeLeft <= 1)
        {
            countDownText.text = (1).ToString("0");
            Invoke("CountDownEnded", 0.5f);

        }
    }
    void CountDownEnded()
    {
        countDownText.enabled = false;

    }
}
