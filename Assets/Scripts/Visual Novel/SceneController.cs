using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SceneController : MonoBehaviour
{
    public ChapterScene currentScene;
    public TextBoxController textbox;

    private int choiceIndex = 0;
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
                choiceIndex++;
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
                if (choiceIndex < textbox.currentScene.choices.Count)
                {
                    if (textbox.currentScene.choices[choiceIndex].sentenceNumber == textbox.GetSentenceIndex() && isChoice == false)
                    {
                        textbox.decisionButton1.SetActive(true);
                        textbox.decisionButton2.SetActive(true);
                        textbox.decisionButton1.GetComponentInChildren<TextMeshProUGUI>().text = currentScene.choices[choiceIndex].response1;
                        textbox.decisionButton2.GetComponentInChildren<TextMeshProUGUI>().text = currentScene.choices[choiceIndex].response2;
                        isChoice = true;
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
