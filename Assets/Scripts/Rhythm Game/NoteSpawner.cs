using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    public GameObject objectToSpawn;
    public GameObject spawner1;
    public GameObject spawner2;
    public GameObject spawner3;
    public GameObject spawner4;

    private GameObject spawnerToUse;
    private int spawnInterval = 0;
    private int nextSpawn = 0;
    private bool waitForString = false;
    private string stringToWaitFor = "";

    void Awake()
    {
        nextSpawn = spawnInterval;
        ScriptUsageTimeline.MarkerUpdated += WaitForMarker; //subscribing to event
        ScriptUsageTimeline.BeatUpdated += Spawn;
    }

    private void OnDestroy()
    {
        ScriptUsageTimeline.MarkerUpdated -= WaitForMarker; //unsubscribing to event
        ScriptUsageTimeline.BeatUpdated -= Spawn;
    }

    void Start()
    {
        
    }

   
    void Update()
    {
        
    }

    void Spawn()
    {
        if (!waitForString)
        {
            if (nextSpawn > 0)
            {
                nextSpawn--;
            }
            else
            {
                int min = 1;
                int max = 4;
                int spawner = Random.Range(min , max);

                switch(spawner)
                {
                    case 1:
                        spawnerToUse = spawner1;
                        break;

                    case 2:
                        spawnerToUse = spawner2;
                        break;

                    case 3:
                        spawnerToUse = spawner3;
                        break;

                    case 4:
                        spawnerToUse = spawner4;
                        break;
                }
                
                var obj = Instantiate(objectToSpawn, spawnerToUse.transform);
                nextSpawn += spawnInterval /*- 1*/;
                obj.transform.position = spawnerToUse.transform.localPosition;
            }
        }
    }

    void WaitForMarker()
    {
        if (ScriptUsageTimeline.instance.timelineInfo.lastMarker == stringToWaitFor)
        {
            waitForString = false;
        }
    }
}
