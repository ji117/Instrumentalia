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

   [SerializeField] private int sentenceIndex = -1;
    private int choiceNumber = 0;
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

    public void MakeChoice()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayChoiceSentence(0);
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            PlayChoiceSentence(1);
            return;
        }
        MakeChoice(); 
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
