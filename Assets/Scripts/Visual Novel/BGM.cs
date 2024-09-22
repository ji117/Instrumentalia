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
        if (Player.instance == null)
            eventEmitter.EventInstance.setVolume(0.1f);
        else
            eventEmitter.EventInstance.setVolume(Player.instance.GetBGMVolume());
    }

    private void OnDestroy()
    {
        eventEmitter.EventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        eventEmitter.EventInstance.release();
    }
}
