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
    public GameObject gameOverScreen;
    public GameObject levelCompleteScreen;

    void Start()
    {
        
    }

    
    void Update()
    {
        scoreText.text = "Score: " + GameController.gameInstance.GetScore();

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

        if (GameController.gameInstance.IsSongFinished())
        {
            levelCompleteScreen.SetActive(true);
            levelCompleteScoreText.text = "Score: " + GameController.gameInstance.GetScore();
            goodsText.text = "Good x " + GameController.gameInstance.GetGoods();
            perfectsText.text = "Perfects x " + GameController.gameInstance.GetPerfects();
            missText.text = "Miss x " + GameController.gameInstance.GetMisses();
            if (Input.GetKeyDown(KeyCode.R))
            {
                GameController.gameInstance.RestartGame();
            }
        }
    }
}
