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
    public TextMeshProUGUI loadingText;
    public string[] hints;
    public StudioEventEmitter bgmEmitter;
    

    public void StartLoading()
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

        StartCoroutine(LoadLevel());
    }

    IEnumerator LoadLevel()
    {
        bgmEmitter.Stop();
        yield return new WaitForSeconds(5.0f);
        SceneManager.LoadSceneAsync(sceneToLoad);
    }
}
