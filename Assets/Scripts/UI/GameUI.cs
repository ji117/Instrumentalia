using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameUI : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI startText;
    void Start()
    {
        
    }

    
    void Update()
    {
        scoreText.text = "Score: " + GameController.gameInstance.GetScore();

        if (GameController.gameInstance.songStarted)
        {
            startText.gameObject.SetActive(false); 
        }
    }
}
