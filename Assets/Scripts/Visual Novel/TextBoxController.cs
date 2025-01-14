using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using FMODUnity;

public class TextBoxController : MonoBehaviour
{
    public TextMeshProUGUI dialogue;
    public TextMeshProUGUI speakerName;
    public Image speakerPotrait;
    public Image background;
    public Image instrument; 
    public ChapterScene currentScene;
    public GameObject decisionButton1;
    public GameObject decisionButton2;
    public StudioEventEmitter typingEventEmitter;
    public StudioEventEmitter speechEventEmitter;
    public StudioEventEmitter buttonEventEmitter;

    private string playerName;
    private string currentDialogueLine; 
    private float volume;
    private int sentenceIndex = -1;
    private int choiceNumber = 0;
    private float originalDialogueSpeed = 0.02f;
    private float dialogueSpeed = 0.02f;
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
        if (Player.instance == null)
        {
            playerName = "Test";
            volume = 0.1f;
        }
        else
        {
            playerName = Player.instance.GetName();
            volume = Player.instance.GetSFXVolume();
        }

        PlayNextSentence();
    }
    public void PlayScene(ChapterScene scene)
    {
        currentScene = scene;
        background.sprite = currentScene.background;
    }
    public void PlayNextSentence()
    {
        currentDialogueLine = currentScene.sentences[++sentenceIndex].text.Replace(">PLAYER NAME<", playerName);
        StartCoroutine(TypeDialogue(currentDialogueLine));
        speakerName.text = currentScene.sentences[sentenceIndex].speaker.speakerName;
        speakerPotrait.sprite = currentScene.sentences[sentenceIndex].speaker.speakerPotrait;

        if (currentScene.sentences[sentenceIndex].speaker.speakerInstrument != null)
        {
            instrument.gameObject.SetActive(true);
            instrument.sprite = currentScene.sentences[sentenceIndex].speaker.speakerInstrument;
        }
        else
            instrument.gameObject.SetActive(false);

        speechEventEmitter.Play();
        speechEventEmitter.EventInstance.setVolume(volume);
    }

    public void PlayChoiceSentence(int playerChoice)
    {
        currentDialogueLine = currentScene.choices[choiceNumber].choiceSentences[playerChoice].text.Replace(">PLAYER NAME<", playerName);
        StartCoroutine(TypeDialogue(currentDialogueLine));
        speakerName.text = currentScene.choices[choiceNumber].choiceSentences[playerChoice].speaker.speakerName;
        speakerPotrait.sprite = currentScene.choices[choiceNumber].choiceSentences[playerChoice].speaker.speakerPotrait;

        if (currentScene.choices[choiceNumber].choiceSentences[playerChoice].speaker.speakerInstrument != null)
        {
            instrument.gameObject.SetActive(true);
            instrument.sprite = currentScene.choices[choiceNumber].choiceSentences[playerChoice].speaker.speakerInstrument;
        }
        else
            instrument.gameObject.SetActive(false);

        speechEventEmitter.Play();
        speechEventEmitter.EventInstance.setVolume(volume);
        choiceNumber++;
    }

    public bool MakeChoice()
    {
        if (IsDecisionButton1Pressed())
        {
            PlayChoiceSentence(0);
            buttonEventEmitter.Play();
            buttonEventEmitter.EventInstance.setVolume(volume);
            decision1 = false;
            choiceMade = true;
            return choiceMade;
        }

        if (IsDecisionButton2Pressed())
        {
            PlayChoiceSentence(1);
            buttonEventEmitter.Play();
            buttonEventEmitter.EventInstance.setVolume(volume);
            decision2 = false;
            choiceMade = true;
            return choiceMade;
        }
        return choiceMade;
    }

    public string GetCurrentDialogueLine()
    {
        return currentDialogueLine; 
    }

    public void ChoiceMade()
    {
        choiceMade = false; 
    }

    public int GetChoiceIndex()
    {
        return choiceNumber;
    }

    public void ResetChoiceIndex()
    {
        choiceNumber = 0;
    }

    public void ResetSentenceIndex()
    {
        sentenceIndex = -1;
    }

    public int GetSentenceIndex()
    {
        return sentenceIndex;
    }

    public bool IsCompleted()
    {
        return state == State.COMPLETED;
    }

    public void SetComplete()
    {
        state = State.COMPLETED; 
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

    public void SetDialogueSpeed(float newDialogueSpeed)
    {
        dialogueSpeed = newDialogueSpeed;
    }

    public void ResetDialogueSpeed()
    {
        dialogueSpeed = originalDialogueSpeed;
    }


    private IEnumerator TypeDialogue(string text)
    {
        dialogue.text = "";
        state = State.PLAYING;
        int wordIndex = 0;

        typingEventEmitter.Play();
        typingEventEmitter.EventInstance.setVolume(volume);
        while(state != State.COMPLETED)
        {
            if (typingEventEmitter.IsPlaying() == false) //not ideal looping like this unclean
            {
                typingEventEmitter.Play();
                typingEventEmitter.EventInstance.setVolume(volume);
            }
            
            dialogue.text += text[wordIndex];
            yield return new WaitForSeconds(dialogueSpeed);
            if(++wordIndex == text.Length)
            {
                state = State.COMPLETED;
                break;
            }
        }
        typingEventEmitter.Stop();
    }


    private void OnDestroy()
    {
        typingEventEmitter.EventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        typingEventEmitter.EventInstance.release();
        speechEventEmitter.EventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        speechEventEmitter.EventInstance.release();
        buttonEventEmitter.EventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        buttonEventEmitter.EventInstance.release(); 
    }
}
