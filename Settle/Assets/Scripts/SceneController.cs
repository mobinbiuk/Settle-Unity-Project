using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneSwitcher : MonoBehaviour
{
    private Button coinFlipButton;

    void Start()
    {
        // درخواست دکمه از Component Button
        coinFlipButton = GetComponent<Button>();

        // افزودن رویداد به دکمه
        coinFlipButton.onClick.AddListener(OnCoinFlipButtonClick);
    }
    public void OnCoinFlipButtonClick()
    {
        // این متد به عنوان رفتن به scene بعدی فعلی عمل می‌کند.
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        // اگر می‌خواهید به اولین scene بروید اگر در آخرین scene هستید، می‌توانید از کد زیر استفاده کنید:
        // int nextSceneIndex = (currentSceneIndex + 1) % SceneManager.sceneCountInBuildSettings;

        // تاخیر یک زمان مشخص (مثلاً 0.5 ثانیه) قبل از بروندن به scene بعدی
        float delay = 0.5f;
        Invoke("LoadNextScene", delay);
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
