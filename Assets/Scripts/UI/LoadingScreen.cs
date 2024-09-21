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
    public int sceneToLoad;
    public int currentScene;
    public TextMeshProUGUI loadingText;
    public string[] hints;
    public StudioEventEmitter bgmEmitter;
    

    public void StartLoading(int scene)
    {
        game.SetActive(false);
        loadingScreen.SetActive(true);
        int textToLoad = Random.Range(1, 2);

        switch(textToLoad)
        {
            case 1:
                loadingText.text = hints[0];
                break;

            case 2:
                loadingText.text = hints[1];
                break;
        }

        StartCoroutine(LoadLevel(scene));
    }

    IEnumerator LoadLevel(int scene)
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
