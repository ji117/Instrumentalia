using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SceneController : MonoBehaviour
{
    public ChapterScene currentScene;
    public TextBoxController textbox;

    private int choiceIndex = 0;
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
                if (choiceIndex < textbox.currentScene.choices.Count)
                {
                    if (textbox.currentScene.choices[choiceIndex].sentenceNumber == textbox.GetSentenceIndex())
                    {
                        textbox.MakeChoice();
                        choiceIndex++;
                        return;
                    }
                }
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
