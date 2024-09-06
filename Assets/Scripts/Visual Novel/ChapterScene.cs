using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewChapterScene", menuName = "Data/New Chapter Scene")]
[System.Serializable]
public class ChapterScene : ScriptableObject
{
    public List<Sentence> sentences;
    public List<Choice> choices;
    public Sprite background;
    public ChapterScene nextScene;

    [System.Serializable]
    public struct Sentence
    {
        public string text;
        public Speaker speaker; 
    }
    [System.Serializable]
    public struct Choice
    {
        public List<Sentence> choiceSentences;
        public string response1;
        public string response2;
        public int sentenceNumber; 
    }

}
