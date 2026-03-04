using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    public GameObject startUI;
    public GameObject gameplay;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI scoreHighText;
    public GameObject player;
    // public GameObject gameplayRoot;
    float time;
    int scoreTotal = 0;
    int scoreHigh;
    // string scoreHighTx;

    
    void Start()
    {
        gameplay.SetActive(false);
       
        // todo - sign up for notification about enemy death 
        Enemy.OnEnemyDied += OnEnemyDied;
        Player.OnPlayerDied += OnPlayerDied;

        if (!PlayerPrefs.HasKey("scoreHighStored"))
        {
            scoreHigh = 0;
            PlayerPrefs.SetInt("scoreHighStored", scoreHigh);
        }
        else
            scoreHigh = PlayerPrefs.GetInt("scoreHighStored");

        ScoreFormatter(PlayerPrefs.GetInt("scoreHighStored"), scoreHighText);
        time = 0;

    }

    void Update()
    {
        if (time <= 3)
            StartScreen();
    }

    void OnEnemyDied(int points)
    {
        Debug.Log($"Killed enemy, worth: {points}");

        scoreTotal += points;
        ScoreFormatter(scoreTotal, scoreText);
    }

    void OnDestroy()
    {
        Enemy.OnEnemyDied -= OnEnemyDied;
    }

    void StartScreen()
    {
        time += Time.deltaTime;
        if (time >= 3 || Mouse.current.leftButton.wasPressedThisFrame)
        {
            startUI.SetActive(false);
            gameplay.SetActive(true);
        }
    }

    void OnPlayerDied()
    {
        Debug.Log("running");
        if (scoreTotal > scoreHigh)
        {
            scoreHigh = scoreTotal;
            PlayerPrefs.SetInt("scoreHighStored", scoreHigh);

            ScoreFormatter(PlayerPrefs.GetInt("scoreHighStored"), scoreHighText);
            PlayerPrefs.Save();
        }

        Transform playerInstance = Instantiate(player).transform;
        playerInstance.transform.position = new Vector2(0, -10);
        playerInstance.transform.parent = gameplay.transform;
        
        startUI.SetActive(true);
        gameplay.SetActive(false);
        
        time = 0;
        scoreTotal = 0;
        ScoreFormatter(scoreTotal, scoreText);
    
    }

    void ScoreFormatter(int points, TextMeshProUGUI field)
    {
        
        if(points < 10)
            field.text = $"000{points}";
        else if(points < 100)
            field.text = $"00{points}";
        else if(scoreTotal < 1000)
            field.text = $"0{points}";
        else
            field.text = $"{points}";
    }

}
