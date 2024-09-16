using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController gameInstance;
    [SerializeField] int score = 0;
    [SerializeField] int misses = 0;
    [SerializeField] int goods = 0;
    [SerializeField] int perfects = 0;
    bool songFinished = false; 
    void Awake()
    {
        gameInstance = this;
    }

    
    void Update()
    {
        if (misses >= 10)
        {
            Debug.Log("Game Over!");
        }

        if (songFinished)
        {
            Debug.Log("Song Finished");
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
}
