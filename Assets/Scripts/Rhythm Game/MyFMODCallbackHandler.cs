using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD;
using FMODUnity;
using System;

[CreateAssetMenu(menuName = "My FMOD Callback Handler")]
public class MyFMODCallbackHandler : FMODUnity.PlatformCallbackHandler
{
    public override void PreInitialize(FMOD.Studio.System studioSystem, Action<FMOD.RESULT, string> reportResult)
    {
        FMOD.RESULT result;

        FMOD.System coreSystem;
        result = studioSystem.getCoreSystem(out coreSystem);
        reportResult(result, "studioSystem.getCoreSystem");
    }
}
