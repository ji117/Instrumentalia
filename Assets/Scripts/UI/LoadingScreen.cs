using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using FMODUnity;

public class LoadingScreen : MonoBehaviour
{
    public GameObject game;
    public GameObject loadingScreen;
    public string sceneToLoad;
    public string currentScene;
    public TextMeshProUGUI loadingText;
    public string[] hints;
    public StudioEventEmitter bgmEmitter;
    

    public void StartLoading(string scene)
    {
        game.SetActive(false);
        loadingScreen.SetActive(true);
        int textToLoad = Random.Range(0, hints.Length); 
        loadingText.text = hints[textToLoad];
        StartCoroutine(LoadLevel(scene));
    }

    IEnumerator LoadLevel(string scene)
    {
        if (game.CompareTag("Game"))
        {
            ScriptUsageTimeline.instance.GetMusicInstance().stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        }
        else 
        bgmEmitter.EventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        yield return new WaitForSeconds(3.0f);
        SceneManager.LoadSceneAsync(scene);
    }
}
