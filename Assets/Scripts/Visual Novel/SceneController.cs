using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SceneController : MonoBehaviour
{
    public ChapterScene currentScene;
    public TextBoxController textbox;
    void Start()
    {
        textbox.PlayScene(currentScene);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            if (textbox.IsCompleted())
            {
                if (textbox.IsLastSentence())
                {
                    currentScene = currentScene.nextScene;
                    textbox.PlayScene(currentScene); 
                }
                textbox.PlayNextSentence();
            }
        }
    }
}
