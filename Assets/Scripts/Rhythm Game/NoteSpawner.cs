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
    private int noteCase = 1;
    private bool waitForString = false;
    private string stringToWaitFor = "";
    private bool spawnNote = false;

    void Awake()
    {
        nextSpawn = spawnInterval;
        ScriptUsageTimeline.MarkerUpdated += WaitForMarker; //subscribing to event
        ScriptUsageTimeline.BarUpdated += Spawn;
        ScriptUsageTimeline.BeatUpdated += Level1; 
    }

    private void OnDestroy()
    {
        ScriptUsageTimeline.MarkerUpdated -= WaitForMarker; //unsubscribing to event
        ScriptUsageTimeline.BarUpdated -= Spawn;
        ScriptUsageTimeline.BeatUpdated -= Level1;
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
                int max = 5;
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
                nextSpawn += spawnInterval;
                obj.transform.position = spawnerToUse.transform.localPosition;
            }
        }
    }

    void Level1()
    {
        switch (noteCase)
        {
            case 1:
                if (ScriptUsageTimeline.instance.timelineInfo.currentMusicBar == 4 && ScriptUsageTimeline.instance.timelineInfo.currentMusicBeat == 3)
                {
                    spawnNote = true;
                    noteCase++; 
                }
                break;

            case 2:
                if (ScriptUsageTimeline.instance.timelineInfo.currentMusicBar == 5 && ScriptUsageTimeline.instance.timelineInfo.currentMusicBeat == 2)
                {
                    spawnNote = true;
                    noteCase++;
                }
                break;


            case 3:
                if (ScriptUsageTimeline.instance.timelineInfo.currentMusicBar == 6 && ScriptUsageTimeline.instance.timelineInfo.currentMusicBeat == 1)
                {
                    spawnNote = true;
                    noteCase++;
                }
                break;

            case 4:
                if (ScriptUsageTimeline.instance.timelineInfo.currentMusicBar == 7 && ScriptUsageTimeline.instance.timelineInfo.currentMusicBeat == 3)
                {
                    spawnNote = true;
                    noteCase++;
                }
                break;

            case 5:
                if (ScriptUsageTimeline.instance.timelineInfo.currentMusicBar == 7 && ScriptUsageTimeline.instance.timelineInfo.currentMusicBeat == 4)
                {
                    spawnNote = true;
                    noteCase++;
                }
                break;

            case 6:
                if (ScriptUsageTimeline.instance.timelineInfo.currentMusicBar == 9 && ScriptUsageTimeline.instance.timelineInfo.currentMusicBeat == 2)
                {
                    spawnNote = true;
                    noteCase++;
                }
                break;

            case 7:
                if(ScriptUsageTimeline.instance.timelineInfo.currentMusicBar == 15 && ScriptUsageTimeline.instance.timelineInfo.currentMusicBeat == 2)
                {
                    spawnNote = true;
                    noteCase++;
                }
                break;

            case 8:
                if (ScriptUsageTimeline.instance.timelineInfo.currentMusicBar == 30 && ScriptUsageTimeline.instance.timelineInfo.currentMusicBeat == 2)
                {
                    spawnNote = true;
                    noteCase++;
                }
                break;

            case 9:
                if (ScriptUsageTimeline.instance.timelineInfo.currentMusicBar == 32 && ScriptUsageTimeline.instance.timelineInfo.currentMusicBeat == 2)
                {
                    spawnNote = true;
                    noteCase++;
                }
                break;

            case 10:
                if (ScriptUsageTimeline.instance.timelineInfo.currentMusicBar == 37 && ScriptUsageTimeline.instance.timelineInfo.currentMusicBeat == 1)
                {
                    spawnNote = true;
                    noteCase++;
                }
                break;

            case 11:
                if (ScriptUsageTimeline.instance.timelineInfo.currentMusicBar == 46 && ScriptUsageTimeline.instance.timelineInfo.currentMusicBeat == 3)
                {
                    spawnNote = true;
                    noteCase++;
                }
                break;

            case 12:
                if (ScriptUsageTimeline.instance.timelineInfo.currentMusicBar == 54 && ScriptUsageTimeline.instance.timelineInfo.currentMusicBeat == 1)
                {
                    spawnNote = true;
                    noteCase++;
                }
                break;

            case 13:
                if (ScriptUsageTimeline.instance.timelineInfo.currentMusicBar == 57 && ScriptUsageTimeline.instance.timelineInfo.currentMusicBeat == 2)
                {
                    spawnNote = true;
                    noteCase++;
                }
                break;

            case 14:
                if (ScriptUsageTimeline.instance.timelineInfo.currentMusicBar == 58 && ScriptUsageTimeline.instance.timelineInfo.currentMusicBeat == 2)
                {
                    spawnNote = true;
                    noteCase++;
                }
                break;

            case 15:
                if (ScriptUsageTimeline.instance.timelineInfo.currentMusicBar == 62 && ScriptUsageTimeline.instance.timelineInfo.currentMusicBeat == 3)
                {
                    spawnNote = true;
                    noteCase++;
                }
                break;
        }

        if (spawnNote == true)
        {
            int min = 1;
            int max = 5;
            int spawner = Random.Range(min, max);

            switch (spawner)
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
            spawnNote = false;
            obj.transform.position = spawnerToUse.transform.localPosition; 
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
