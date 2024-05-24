using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class WordDataOperations
{
    private WordDataList wordDataList;
    public WordDataOperations()
    {
        wordDataList = WordDataList.LoadWordDataList();
    }

    public WordDataList GetWordDataList()
    {
        return wordDataList;
    }

    public void CreateAndAddWordToList(string wordEnglish, string wordTurkish, string wordSentenceExamples, string soundFilePath, string imageFilePath)
    {
        WordData wordData = new WordData(this.wordDataList.wordDatas.Count + 1, wordEnglish, wordTurkish, wordSentenceExamples);
        CopySoundDataToDatabase(soundFilePath, wordData.wordSoundCount);
        CopyImageDataToDatabase(imageFilePath, wordData.wordImageCount);
        this.wordDataList.wordDatas.Add(wordData);
        SaveList();
    }

    public string CopySoundDataToDatabase(string soundFilePath, int wordSoundCount)
    {
        if (!Directory.Exists(Application.persistentDataPath + "/" + "WordDataSave" + "/" + "WordDataSounds"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/" + "WordDataSave" + "/" + "WordDataSounds");
            Debug.Log("Klasör oluþturuldu: " + Application.persistentDataPath + "/" + "WordDataSave" + "/" + "WordDataSounds");
        }
        string databasePath = Application.persistentDataPath + "/" + "WordDataSave" + "/" + "WordDataSounds" + "/" + Convert.ToString(wordSoundCount) + ".mp3";
        File.Copy(soundFilePath, databasePath, true);
        return databasePath;
    }

    public string CopyImageDataToDatabase(string imageFilePath, int wordImageCount)
    {
        if (!Directory.Exists(Application.persistentDataPath + "/" + "WordDataSave" + "/" + "WordDataImages"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/" + "WordDataSave" + "/" + "WordDataImages");
            Debug.Log("Klasör oluþturuldu: " + Application.persistentDataPath + "/" + "WordDataSave" + "/" + "WordDataImages");
        }
        string databasePath = Application.persistentDataPath + "/" + "WordDataSave" + "/" + "WordDataImages" + "/" + Convert.ToString(wordImageCount) + ".png";
        File.Copy(imageFilePath, databasePath, true);
        return imageFilePath;
    }

    public void SaveList()
    {
        WordDataList.SaveWordDataList(this.wordDataList);
    }
}
