using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WordData
{
    public int wordID;
    public string wordEnglish;
    public string wordTurkish;
    public string wordSentenceExamples;
    public int wordSoundCount;
    public int wordImageCount;

    public WordData(int wordID, string wordEnglish, string wordTurkish, string wordSentenceExamples)
    {
        this.wordID = wordID;
        this.wordEnglish = wordEnglish;
        this.wordTurkish = wordTurkish;
        this.wordSentenceExamples = wordSentenceExamples;
        this.wordSoundCount = wordID;
        this.wordImageCount = wordID;
    }

    public static string GetWordSoundPath(WordData wordData)
    {
        string soundPath = Application.persistentDataPath + "/" + "WordDataSave" + "/" + "WordDataSounds" + "/" + Convert.ToString(wordData.wordSoundCount) + ".mp3";
        return soundPath;
    }

    public static string GetWordImagePath(WordData wordData)
    {
        string imagePath = Application.persistentDataPath + "/" + "WordDataSave" + "/" + "WordDataImages" + "/" + Convert.ToString(wordData.wordImageCount) + ".png";
        return imagePath;
    }
}
