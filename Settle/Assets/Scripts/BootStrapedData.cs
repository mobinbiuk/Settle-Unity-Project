using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class PerformBootstrap
{
    const string sceneName = "BootStrap";

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void ExecuteBootstrap()
    {
        for (int sceneIndex=0; sceneIndex <SceneManager.sceneCount; ++sceneIndex)
        {
            var candidate=SceneManager.GetSceneAt(sceneIndex);
            //check if bootstrap is loaded
            if(candidate.name == sceneName)
            {
                return;
            }
            //additively load the bootstrap scene
            SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
        }
    }
}


public class BootStrapedData : MonoBehaviour
{
    public static BootStrapedData Instance { get; private set; } = null;
    
    void Awake()
    {
        //check if instance exists
        if (Instance != null)
        {
            Debug.LogError("found another bootstraped data on:" + gameObject.name);
            Destroy(gameObject);
            return;
        }

        Instance = this;
        //bootstrap scene will remain
        DontDestroyOnLoad(gameObject);
    }
    public void test() 
    {
        Debug.Log("bootstrap is working!");
    }

}
