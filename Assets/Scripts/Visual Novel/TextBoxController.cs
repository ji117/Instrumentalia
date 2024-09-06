using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextBoxController : MonoBehaviour
{
    public TextMeshProUGUI dialogue;
    public TextMeshProUGUI speakerName;
    public Image speakerPotrait;
    public Image background; 
    public ChapterScene currentScene;
    public GameObject decisionButton1;
    public GameObject decisionButton2;

    private int sentenceIndex = -1;
    private int choiceNumber = 0;
    private bool decision1 = false;
    private bool decision2 = false;
    private bool choiceMade = false;
    private State state = State.COMPLETED;

    private enum State
    {
        PLAYING, COMPLETED
    }

    private void Start()
    {
        PlayNextSentence();
    }
    public void PlayScene(ChapterScene scene)
    {
        currentScene = scene;
        background.sprite = currentScene.background;
        sentenceIndex = -1;
    }
    public void PlayNextSentence()
    {
        StartCoroutine(TypeDialogue(currentScene.sentences[++sentenceIndex].text));
        speakerName.text = currentScene.sentences[sentenceIndex].speaker.speakerName;
        speakerPotrait.sprite = currentScene.sentences[sentenceIndex].speaker.speakerPotrait;
    }

    public void PlayChoiceSentence(int playerChoice)
    {
        StartCoroutine(TypeDialogue(currentScene.choices[choiceNumber].choiceSentences[playerChoice].text));
        speakerName.text = currentScene.choices[choiceNumber].choiceSentences[playerChoice].speaker.speakerName;
        speakerPotrait.sprite = currentScene.choices[choiceNumber].choiceSentences[playerChoice].speaker.speakerPotrait;
        choiceNumber++;
    }

    public bool MakeChoice()
    {
        if (IsDecisionButton1Pressed())
        {
            PlayChoiceSentence(0);
            decision1 = false;
            choiceMade = true;
            return choiceMade;
        }

        if (IsDecisionButton2Pressed())
        {
            PlayChoiceSentence(1);
            decision2 = false;
            choiceMade = true;
            return choiceMade;
        }
        return choiceMade;
    }

    public void ChoiceMade()
    {
        choiceMade = false; 
    }

    public int GetSentenceIndex()
    {
        return sentenceIndex;
    }
    public bool IsCompleted()
    {
        return state == State.COMPLETED;
    }

    public bool IsLastSentence()
    {
        return sentenceIndex + 1 == currentScene.sentences.Count;
    }

    private bool IsDecisionButton1Pressed()
    {
        return decision1;
    }

    private bool IsDecisionButton2Pressed()
    {
        return decision2;
    }

    public void DecisionButton1Pressed()
    {
        decision1 = true;
    }

    public void DecisionButton2Pressed()
    {
        decision2 = true; 
    }

    private IEnumerator TypeDialogue(string text)
    {
        dialogue.text = "";
        state = State.PLAYING;
        int wordIndex = 0;

        while(state != State.COMPLETED)
        {
            dialogue.text += text[wordIndex];
            yield return new WaitForSeconds(0.05f);
            if(++wordIndex == text.Length)
            {
                state = State.COMPLETED;
                break;
            }
        }
    }
}
