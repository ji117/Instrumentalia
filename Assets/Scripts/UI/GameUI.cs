using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameUI : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI startText;
    public GameObject gameOverScreen; 

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
            gameOverScreen.SetActive(true);
            if (Input.GetKeyDown(KeyCode.R))
            {
                GameController.gameInstance.RestartGame();
            }
        }
    }
}
