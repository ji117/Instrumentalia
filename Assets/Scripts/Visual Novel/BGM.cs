using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class BGM : MonoBehaviour
{
    public StudioEventEmitter eventEmitter;
    void Start()
    {
        eventEmitter.Play();
        eventEmitter.EventInstance.setVolume(0.1f);
    }
}
