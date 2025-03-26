using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    int score;
    int lives;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI GameOverText;
    public GameObject newGameText;
    public bool gameIsOver;

    public TextMeshProUGUI timer;
    public float timeRemaining = 5f; // in seconds 

    void Start()
    {
        score = 0;
        lives = 3;
        UpdateTimeDisplay(timeRemaining);
        newGameText.SetActive(false);
        GameOverText.gameObject.SetActive(false);
    }

    public void UpdateTheScore(int scorePointsAdd)
    {
        score += scorePointsAdd;
        scoreText.text = "Score: " + score.ToString();
    }

    public void UpdateLives()
    {
        if (!gameIsOver)
        {
            lives--;
            livesText.text = "Lives: " + lives;

            if (lives <= 0)
            {
                GameOver();
            }
        }
    }

    void Update()
    {
        if (!gameIsOver && timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            UpdateTimeDisplay(timeRemaining);
        }
        else if (timeRemaining <= 0 && !gameIsOver)
        {
            UpdateTimeDisplay(0);
            GameOver();
        }
    }

    void UpdateTimeDisplay(float time)
    {
        timer.text = "Time: " + Mathf.Ceil(time).ToString();
    }

    public void GameOver()
    {
        gameIsOver = true;
        scoreText.gameObject.SetActive(false);
        timer.gameObject.SetActive(false);
        livesText.gameObject.SetActive(false);
        GameOverText.gameObject.SetActive(true);

        StartCoroutine(ShowGameOverText());
    }

    IEnumerator ShowGameOverText()
    {
       
        GameOverText.gameObject.SetActive(true);
        
        
        yield return new WaitForSeconds(2f);
        newGameText.SetActive(true);

        
        yield return new WaitForSeconds(2f);
        RestartGame();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

