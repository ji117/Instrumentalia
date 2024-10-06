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
    public Sprite A;
    public Sprite S;
    public Sprite D;
    public Sprite F;
    public Sprite H;
    public Sprite J;
    public Sprite K;
    public Sprite L;
    public LoadingScreen loadingScreen;
    public GameObject missEffect;
    public GameObject goodEffect;
    public Transform effectTransform; 
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
                area1.GetComponent<SpriteRenderer>().sprite = H;
                area2.key = KeyCode.J;
                area2.GetComponent<SpriteRenderer>().sprite = J;
                area3.key = KeyCode.K;
                area3.GetComponent<SpriteRenderer>().sprite = K;
                area4.key = KeyCode.L;
                area4.GetComponent<SpriteRenderer>().sprite = L;
            }
            else
            {
                area1.key = KeyCode.A;
                area1.GetComponent<SpriteRenderer>().sprite = A;
                area2.key = KeyCode.S;
                area2.GetComponent<SpriteRenderer>().sprite = S;
                area3.key = KeyCode.D;
                area3.GetComponent<SpriteRenderer>().sprite = D;
                area4.key = KeyCode.F;
                area4.GetComponent<SpriteRenderer>().sprite = F;
            }
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
        var obj = Instantiate(missEffect);
    }

    public void AddGood()
    {
        goods++;
        var obj = Instantiate(goodEffect);
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
