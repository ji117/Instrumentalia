using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameUI : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI startText;
    public TextMeshProUGUI levelCompleteScoreText;
    public TextMeshProUGUI missText;
    public TextMeshProUGUI goodsText;
    public TextMeshProUGUI perfectsText;
    public TextMeshProUGUI repText;
    public TextMeshProUGUI timerText; 
    public GameObject gameOverScreen;
    public GameObject levelCompleteScreen;
    public GameObject pauseScreen; 

    private float endGameTimer = 10.0f; 

    void Start()
    {
        
    }

    
    void Update()
    {
        scoreText.text = "" + GameController.gameInstance.GetScore();

        if (GameController.gameInstance.IsSongStarted())
        {
            startText.gameObject.SetActive(false); 
        }

        if (GameController.gameInstance.IsGameOver())
        {
            gameOverScreen.SetActive(true);
            if (Input.GetKeyDown(KeyCode.R))
            {
                GameController.gameInstance.RestartGame(); 
            }
        }

        if (GameController.gameInstance.IsSongFinished() && endGameTimer < 0f)
        {
            levelCompleteScreen.SetActive(true);
            levelCompleteScoreText.text = GameController.gameInstance.GetScore().ToString();
            goodsText.text = "x " + GameController.gameInstance.GetGoods();
            perfectsText.text = "x " + GameController.gameInstance.GetPerfects();
            missText.text = "x " + GameController.gameInstance.GetMisses();
            repText.text = "+ " + GameController.gameInstance.CalculateReputation();
            if (Input.GetKeyDown(KeyCode.R))
            {
                GameController.gameInstance.RestartGame();
            }
        }

        if (GameController.gameInstance.IsUnpauseTimerStarted())
        {
            if (GameController.gameInstance.GetUnpauseTimer() <= 0)
                GameController.gameInstance.UnpauseGame();

            timerText.gameObject.SetActive(true);
            timerText.text = Mathf.RoundToInt(GameController.gameInstance.GetUnpauseTimer()).ToString();
        }
    }

    private void FixedUpdate()
    {
        if (GameController.gameInstance.IsSongFinished() && !GameController.gameInstance.IsGamePaused())
        {
            endGameTimer =  endGameTimer - 0.1f;
        }

        if (GameController.gameInstance.IsGamePaused())
            pauseScreen.SetActive(true);
        else
        {
            pauseScreen.SetActive(false);
            timerText.gameObject.SetActive(false);
        }
    }
}
