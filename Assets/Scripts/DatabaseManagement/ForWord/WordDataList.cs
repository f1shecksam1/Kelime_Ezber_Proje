using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class WordDataList
{
    public List<WordData> wordDatas;

    public WordDataList()
    {
        wordDatas = new List<WordData>();
    }

    public static void SaveWordDataList(WordDataList wordDataList)
    {
        string path = Application.persistentDataPath + "/" + "WordDataSave" + "/" + "WordDataList" + ".json";
        string json = JsonConvert.SerializeObject(wordDataList);
        File.WriteAllText(path, json);
        Debug.Log(Application.persistentDataPath);
    }

    public static WordDataList LoadWordDataList()
    {
        string path = Application.persistentDataPath + "/" + "WordDataSave" + "/" + "WordDataList" + ".json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<WordDataList>(json);
        }
        else
        {
            Debug.LogWarning("Save file not found in " + path);
            WordDataList emptyUserDataList = new WordDataList();
            emptyUserDataList.wordDatas.Add(new WordData(1, "car", "araba", "The car is beautiful."));
            emptyUserDataList.wordDatas.Add(new WordData(2, "house", "ev", "The house is big."));
            emptyUserDataList.wordDatas.Add(new WordData(3, "book", "kitap", "This book is interesting."));
            emptyUserDataList.wordDatas.Add(new WordData(4, "computer", "bilgisayar", "I need a new computer."));
            emptyUserDataList.wordDatas.Add(new WordData(5, "phone", "telefon", "My phone is old."));
            emptyUserDataList.wordDatas.Add(new WordData(6, "dog", "köpek", "The dog is friendly."));
            emptyUserDataList.wordDatas.Add(new WordData(7, "cat", "kedi", "The cat is sleeping."));
            emptyUserDataList.wordDatas.Add(new WordData(8, "tree", "aðaç", "The tree is tall."));
            emptyUserDataList.wordDatas.Add(new WordData(9, "river", "nehir", "The river is flowing."));
            emptyUserDataList.wordDatas.Add(new WordData(10, "mountain", "dað", "The mountain is high."));
            emptyUserDataList.wordDatas.Add(new WordData(11, "flower", "çiçek", "The flower is blooming."));
            emptyUserDataList.wordDatas.Add(new WordData(12, "sun", "güneþ", "The sun is shining."));
            emptyUserDataList.wordDatas.Add(new WordData(13, "moon", "ay", "The moon is bright."));
            emptyUserDataList.wordDatas.Add(new WordData(14, "star", "yýldýz", "The star is twinkling."));
            emptyUserDataList.wordDatas.Add(new WordData(15, "sea", "deniz", "The sea is calm."));
            emptyUserDataList.wordDatas.Add(new WordData(16, "lake", "göl", "The lake is clear."));
            emptyUserDataList.wordDatas.Add(new WordData(17, "bird", "kuþ", "The bird is singing."));
            emptyUserDataList.wordDatas.Add(new WordData(18, "fish", "balýk", "The fish is swimming."));
            emptyUserDataList.wordDatas.Add(new WordData(19, "apple", "elma", "The apple is red."));
            emptyUserDataList.wordDatas.Add(new WordData(20, "banana", "muz", "The banana is yellow."));
            WordDataList.SaveWordDataList(emptyUserDataList);
            return emptyUserDataList;
        }
    }
}
