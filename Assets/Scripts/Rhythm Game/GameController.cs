using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController gameInstance;
    public LoadingScreen loadingScreen;
    [SerializeField] int score = 0;
    [SerializeField] int misses = 0;
    [SerializeField] int goods = 0;
    [SerializeField] int perfects = 0;
    bool songFinished = false;
    bool songStarted = false;
    bool gameOver = false;
    [SerializeField] bool gamePaused;
    void Awake()
    {
        gameInstance = this;
        gamePaused = false;
    }


    void Update()
    {
        if (Input.anyKeyDown && !songStarted)
        {
            ScriptUsageTimeline.instance.StartGame();
            songStarted = true; 
        }

        if (misses >= 10)
        {
            gameOver = true;
        }

        //if (songFinished)
        //{
           
        //}

        if (Input.GetKeyDown(KeyCode.I))
        {
            songFinished = true; 
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (gamePaused)
                gamePaused = false;
            else
                gamePaused = true;
        }

    }

    public void AddScore(int scoreToAdd)
    {
        score = score + scoreToAdd;
    }

    public void AddMiss()
    {
        misses++;
    }

    public void AddGood()
    {
        goods++;
    }

    public void AddPerfect()
    {
        perfects++;
    }

    public int GetScore()
    {
        return score;
    }

    public int GetGoods()
    {
        return goods;
    }

    public int GetPerfects()
    {
        return perfects;
    }

    public int GetMisses()
    {
        return misses;
    }

    public void SongFinished()
    {
        songFinished = true;
    }

    public void GameOver()
    {
        gameOver = true;
    }

    public bool IsSongFinished()
    {
        return songFinished;
    }

    public bool IsGameOver()
    {
        return gameOver; 
    }

    public bool IsSongStarted()
    {
        return songStarted; 
    }

    public bool IsGamePaused()
    {
        return gamePaused;
    }

    public void RestartGame()
    {
        loadingScreen.StartLoading(loadingScreen.currentScene); 
    }

    public int GetMaxScore()
    {
        int maxScore = (GetGoods() + GetPerfects() + GetMisses()) * 200;
        return maxScore; 
    }

    public int CalculateReputation()
    {
        float temp = (float)GetScore() / (float)GetMaxScore() * 100;
        int repGain = Mathf.RoundToInt(temp);
        if (Player.instance == null)
        {
            return repGain;
        }
        else
        Player.instance.AddReputation(repGain);
        return repGain;
    }
}
