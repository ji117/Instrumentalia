using System;
using System.Runtime.InteropServices;
using UnityEngine;
using FMOD;
using FMODUnity;

class ScriptUsageTimeline : MonoBehaviour
{
    public class TimelineInfo
    {
        public int currentMusicBar = 0;
        public int currentMusicBeat = 0;
        public FMOD.StringWrapper lastMarker = new FMOD.StringWrapper();
    }

    public TimelineInfo timelineInfo;
    GCHandle timelineHandle;

    public FMODUnity.EventReference EventName;
    public StudioEventEmitter eventEmitter;
    public static ScriptUsageTimeline instance;

    private FMOD.Studio.EVENT_CALLBACK beatCallback;
    private FMOD.Studio.EventInstance musicInstance;

    public delegate void BeatEventDelegate();
    public static event BeatEventDelegate BeatUpdated;

    public delegate void MarkerListenerDelegate();
    public static event MarkerListenerDelegate MarkerUpdated; 

    public static int lastBeat = 0;
    public static string lastMarkerString = null;

    private void Awake()
    {
        instance = this; 
    }

    void Start()
    {
        
       
       
        
    }

    public void StartGame()
    {
        timelineInfo = new TimelineInfo();

        // Explicitly create the delegate object and assign it to a member so it doesn't get freed
        // by the garbage collected while it's being used
        beatCallback = new FMOD.Studio.EVENT_CALLBACK(BeatEventCallback);

        musicInstance = FMODUnity.RuntimeManager.CreateInstance(EventName);
        UnityEngine.Debug.Log("Event Found");
        // Pin the class that will store the data modified during the callback
        timelineHandle = GCHandle.Alloc(timelineInfo, GCHandleType.Pinned);
        // Pass the object through the userdata of the instance
        musicInstance.setUserData(GCHandle.ToIntPtr(timelineHandle));

        musicInstance.setCallback(beatCallback, FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_BEAT | FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_MARKER);
        musicInstance.start();
        musicInstance.setVolume(0.1f);
    }

    private void Update()
    {
        if (lastMarkerString != timelineInfo.lastMarker)
        {
            lastMarkerString = timelineInfo.lastMarker;

            if (MarkerUpdated != null)
            {
                MarkerUpdated();
            }
        }

        if (lastBeat != timelineInfo.currentMusicBar)
        {
            lastBeat = timelineInfo.currentMusicBar;

            if (BeatUpdated != null)
            {
                BeatUpdated();
            }
        }

        //if(musicInstance.) todo
        //{
        //    GameController.gameInstance.SongFinished();
        //}
    }

    void OnDestroy()
    {
        musicInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        musicInstance.release();
        UnityEngine.Debug.Log("Instance Destroyed");
    }

    void OnGUI()
    {
       GUILayout.Box(String.Format("Current Bar = {0}, Current Beat = {1}, Last Marker = {2}", timelineInfo.currentMusicBar, timelineInfo.currentMusicBeat, (string)timelineInfo.lastMarker));
       // GUILayout.Box(String.Format("Current Beat = {0}", timelineInfo.CurrentMusicBeat));
    }

    [AOT.MonoPInvokeCallback(typeof(FMOD.Studio.EVENT_CALLBACK))]
    static FMOD.RESULT BeatEventCallback(FMOD.Studio.EVENT_CALLBACK_TYPE type, IntPtr instancePtr, IntPtr parameterPtr)
    {
        FMOD.Studio.EventInstance instance = new FMOD.Studio.EventInstance(instancePtr);

        // Retrieve the user data
        IntPtr timelineInfoPtr;
        FMOD.RESULT result = instance.getUserData(out timelineInfoPtr);
        if (result != FMOD.RESULT.OK)
        {
            UnityEngine.Debug.LogError("Timeline Callback error: " + result);
        }
        else if (timelineInfoPtr != IntPtr.Zero)
        {
            // Get the object to store beat and marker details
            GCHandle timelineHandle = GCHandle.FromIntPtr(timelineInfoPtr);
            TimelineInfo timelineInfo = (TimelineInfo)timelineHandle.Target;
            switch (type)
            {
                case FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_BEAT:
                    {
                        var parameter = (FMOD.Studio.TIMELINE_BEAT_PROPERTIES)Marshal.PtrToStructure(parameterPtr, typeof(FMOD.Studio.TIMELINE_BEAT_PROPERTIES));
                        timelineInfo.currentMusicBar = parameter.bar;
                        timelineInfo.currentMusicBeat = parameter.beat; 
                        break;
                    }
                case FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_MARKER:
                    {
                        var parameter = (FMOD.Studio.TIMELINE_MARKER_PROPERTIES)Marshal.PtrToStructure(parameterPtr, typeof(FMOD.Studio.TIMELINE_MARKER_PROPERTIES));
                        timelineInfo.lastMarker = parameter.name;
                        break;
                    }
                case FMOD.Studio.EVENT_CALLBACK_TYPE.DESTROYED:
                    {
                        // Now the event has been destroyed, unpin the timeline memory so it can be garbage collected
                        timelineHandle.Free();
                        break;
                    }
            }
        }
        return FMOD.RESULT.OK;
    }
}
