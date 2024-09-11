using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] int score = 0;
    [SerializeField] int misses = 0;
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    public void AddScore(int scoreToAdd)
    {
        score = score + scoreToAdd;
    }

    public void AddMiss()
    {
        misses++;
    }
}
