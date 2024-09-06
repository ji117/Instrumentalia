using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SceneController : MonoBehaviour
{
    public ChapterScene currentScene;
    public TextBoxController textbox;

    
    private bool isChoice = false;
    

    void Start()
    {
        textbox.PlayScene(currentScene);
    }
    void Update()
    {
        if (isChoice)
        {
            if (textbox.MakeChoice())
            {
                textbox.IncrementChoiceIndex();
                isChoice = false;
                textbox.ChoiceMade();
                textbox.decisionButton1.SetActive(false);
                textbox.decisionButton2.SetActive(false);
            }
            return;
        }
        
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            if (textbox.IsCompleted())
            {
                if (textbox.GetChoiceIndex() < textbox.currentScene.choices.Count)
                {
                    if (textbox.currentScene.choices[textbox.GetChoiceIndex()].sentenceNumber == textbox.GetSentenceIndex() /*&& isChoice == false*/)
                    {
                        textbox.decisionButton1.SetActive(true);
                        textbox.decisionButton2.SetActive(true);
                        textbox.decisionButton1.GetComponentInChildren<TextMeshProUGUI>().text = currentScene.choices[textbox.GetChoiceIndex()].response1;
                        textbox.decisionButton2.GetComponentInChildren<TextMeshProUGUI>().text = currentScene.choices[textbox.GetChoiceIndex()].response2;
                        isChoice = true;
                        return;
                    }
                }
                if (textbox.IsLastSentence())
                {
                    currentScene = currentScene.nextScene;
                    textbox.ResetSentenceIndex();
                    textbox.PlayScene(currentScene); 
                }
                textbox.PlayNextSentence();
            }
        }
    }
}
