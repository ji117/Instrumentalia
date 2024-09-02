using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextBoxController : MonoBehaviour
{
    public TextMeshProUGUI dialogue;
    public TextMeshProUGUI speakerName;
    public Image background; 
    public ChapterScene currentScene;

    private int sentenceIndex = -1;
    private State state = State.COMPLETED;

    private enum State
    {
        PLAYING, COMPLETED
    }

   public void PlayScene(ChapterScene scene)
    {
        currentScene = scene;
        background.sprite = currentScene.background;
        sentenceIndex = -1;
        PlayNextSentence();
    }
    public void PlayNextSentence()
    {
        StartCoroutine(TypeDialogue(currentScene.sentences[++sentenceIndex].text));
        speakerName.text = currentScene.sentences[sentenceIndex].speaker.speakerName;
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
