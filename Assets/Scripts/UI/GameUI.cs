using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameUI : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    void Start()
    {
        
    }

    
    void Update()
    {
        scoreText.text = "Score: " + GameController.gameInstance.GetScore();
    }
}
