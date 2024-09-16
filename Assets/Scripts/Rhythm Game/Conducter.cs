using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class Conducter : MonoBehaviour
{
    float bpm;
    float crotchet; //duration of one beat
    float offset;
    int songPosition;
    FMOD.Studio.EventInstance eventInstance;

    public StudioEventEmitter eventEmitter;


    private void Awake()
    {
        eventInstance = eventEmitter.EventInstance;
    }

    void Start()
    {
         

        //song position = (current time of audio system - dsptime(song length?)) * song.pitch(speedofsong) - offset
        // bpm = eventEmitter.EventInstance

        //crotchet = 1/bps


    }

    
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        
    }

    


}
