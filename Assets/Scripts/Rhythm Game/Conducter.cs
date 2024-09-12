using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class Conducter : MonoBehaviour
{
    float bpm;
    float crotchet; //duration of one beat
    float offset;
    float songposition; 

    void Start()
    {
        //song position = (current time of audio system - dsptime(song length?)) * song.pitch(speedofsong) - offset
        //bpm = 
        //crotchet = 1/bps
    }

    
    void Update()
    {
        
    }
}
