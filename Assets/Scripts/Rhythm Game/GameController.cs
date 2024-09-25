using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController gameInstance;
    public NoteArea area1;
    public NoteArea area2;
    public NoteArea area3;
    public NoteArea area4;
    public LoadingScreen loadingScreen;
    [SerializeField] int score = 0;
    [SerializeField] int misses = 0;
    [SerializeField] int goods = 0;
    [SerializeField] int perfects = 0;
    bool songFinished = false;
    bool songStarted = false;
    bool gameOver = false;
    bool gamePaused;
    bool startUnpauseTimer = false;
    float unpauseTimer = 3.0f; 
    void Awake()
    {
        gameInstance = this;
        gamePaused = false;
    }

    private void Start()
    {
        if (Player.instance != null)
        {
            if (Player.instance.IsRightHanded())
            {
                area1.key = KeyCode.H;
                area2.key = KeyCode.J;
                area3.key = KeyCode.K;
                area4.key = KeyCode.L;
            }
            else
            {
                area1.key = KeyCode.A;
                area2.key = KeyCode.S;
                area3.key = KeyCode.D;
                area4.key = KeyCode.F;
            }
            //todo change sprites depending on keys when added by Jas
        }
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

        if (Input.GetKeyDown(KeyCode.I)) //debug only
        {
            songFinished = true; 
        }

        if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Space)) //might change this to only be space
        {
            if (gamePaused)
                startUnpauseTimer = true;
            else
                gamePaused = true;
        }

    }

    private void FixedUpdate()
    {
        if (startUnpauseTimer)
            unpauseTimer = unpauseTimer - Time.fixedDeltaTime;
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

    public bool IsUnpauseTimerStarted()
    {
        return startUnpauseTimer; 
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

    public void UnpauseGame()
    {
        startUnpauseTimer = false;
        gamePaused = false;
        unpauseTimer = 3.0f; 
    }

    public float GetUnpauseTimer()
    {
        return unpauseTimer; 
    }
}
