using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] int playerLives = 3;
    [SerializeField] int pointValue = 100;

    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] TextMeshProUGUI scoreText;

    int points = 0;

    void Awake()
    {
        int numGameSessions = FindObjectsOfType<LevelManager>().Length;
        if (numGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        livesText.text = playerLives.ToString();
        scoreText.text = points.ToString();
    }

    public void ProcessPlayerDeath()
    {
        if (playerLives > 1)
        {
            TakeLife();
        }
        else
        {
            ResetGame();
        }
    }

    void ResetGame()
    {
        Destroy(FindObjectOfType<LevelPersist>().gameObject);
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }

    void TakeLife()
    {
        playerLives--;
        livesText.text = playerLives.ToString();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ScorePoints()
    {
        points += pointValue;
        scoreText.text = points.ToString();
    }
}
