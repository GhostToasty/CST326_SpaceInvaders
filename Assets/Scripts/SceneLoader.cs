using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    bool creditsPlay;
    float time = 0;
    
    public void Start()
    {
        Player.OnLoadCredits += OnLoadCredits;
    }

    public void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }


    public void Update()
    {
        if (creditsPlay)
        {
            time += Time.deltaTime;
            Debug.Log(time);
            if(time > 5)
            {
                time = 0;
                LoadMenu();
            }
        }
        Debug.Log(creditsPlay);
    }
    
    public void LoadGame()
    {
        StartCoroutine(_LoadGame());
        IEnumerator _LoadGame()
        {
            AsyncOperation loadOperation = SceneManager.LoadSceneAsync("MainGame");
            while(!loadOperation!.isDone) 
                yield return null;
        }
    }

    public void OnLoadCredits()
    {
        StartCoroutine(_LoadCredits());
        IEnumerator _LoadCredits()
        {
            AsyncOperation loadOperation = SceneManager.LoadSceneAsync("Credits");
            while(!loadOperation!.isDone) 
                yield return null;
        }

        creditsPlay = true;
        Debug.Log(true);
    }

    public void LoadMenu()
    {
        creditsPlay = false;
        StartCoroutine(_LoadMenu());
    }

    IEnumerator _LoadMenu()
        {
            AsyncOperation loadOperation = SceneManager.LoadSceneAsync("MainMenu");
            while(!loadOperation!.isDone) 
                yield return null;
        }
    
}
