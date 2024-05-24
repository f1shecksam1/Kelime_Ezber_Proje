using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AskQuestionPageManager : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button previusWordButton;
    [SerializeField] private Button nextWordButton;
    [SerializeField] private Button wordApplyButton;
    [SerializeField] private Button playSoundButton;

    [Header("Text")]
    [SerializeField] private TMP_Text wordTurkishText;
    [SerializeField] private TMP_Text wordExampleSentenceText;

    [Header("Pages")]
    [SerializeField] private GameObject askQuestionPage;
    [SerializeField] private GameObject mainMenuPage;


    [SerializeField] private Image wordReferanceImage;
    [SerializeField] private TMP_InputField wordEnglishIF;

    int questionID;
    int questionCount;
    string wordSoundPath;

    private void Start()
    {
        wordApplyButton.onClick.AddListener(WordApply);
        mainMenuButton.onClick.AddListener(MainMenu);
    }

    private void WordApply()
    {
        if (GameManager.Instance.randomWordIDs.Length > questionCount)
        {
            questionID = GameManager.Instance.randomWordIDs[questionCount];
            if (wordEnglishIF.text == GameManager.Instance.wordDataOperations.GetWordDataList().wordDatas[questionID].wordEnglish)
            {
                Debug.Log("Kelime doðru bilindi");
                GameManager.Instance.userDataOperations.AddWordToKnowedWordDictionary(GameManager.Instance.activeUser, questionID, DateTime.Now);
            }
            else
            {
                Debug.Log("Kelime yanlýþ bilindi");
                GameManager.Instance.userDataOperations.RemoveWordToKnowedWordDictionarys(GameManager.Instance.activeUser, questionID);
            }
            wordTurkishText.text = GameManager.Instance.wordDataOperations.GetWordDataList().wordDatas[questionID].wordTurkish;
            wordExampleSentenceText.text = GameManager.Instance.wordDataOperations.GetWordDataList().wordDatas[questionID].wordSentenceExamples;
            OnFilesSelectedImage(WordData.GetWordImagePath(GameManager.Instance.wordDataOperations.GetWordDataList().wordDatas[questionID]));
            wordSoundPath = WordData.GetWordSoundPath(GameManager.Instance.wordDataOperations.GetWordDataList().wordDatas[questionID]);
        }
        else if (GameManager.Instance.alreadyKnowedWordIDsList.Length > questionCount - GameManager.Instance.randomWordIDs.Length)
        {
            questionID = GameManager.Instance.randomWordIDs[questionCount - GameManager.Instance.randomWordIDs.Length];
            if (wordEnglishIF.text == GameManager.Instance.wordDataOperations.GetWordDataList().wordDatas[questionID].wordEnglish)
            {
                Debug.Log("Kelime doðru bilindi");
                GameManager.Instance.userDataOperations.AddWordToKnowedWordDictionary(GameManager.Instance.activeUser, questionID, DateTime.Now);
            }
            else
            {
                Debug.Log("Kelime yanlýþ bilindi");
                GameManager.Instance.userDataOperations.RemoveWordToKnowedWordDictionarys(GameManager.Instance.activeUser, questionID);
            }
            wordTurkishText.text = GameManager.Instance.wordDataOperations.GetWordDataList().wordDatas[questionID].wordTurkish;
            wordExampleSentenceText.text = GameManager.Instance.wordDataOperations.GetWordDataList().wordDatas[questionID].wordSentenceExamples;
            OnFilesSelectedImage(WordData.GetWordImagePath(GameManager.Instance.wordDataOperations.GetWordDataList().wordDatas[questionID]));
            wordSoundPath = WordData.GetWordSoundPath(GameManager.Instance.wordDataOperations.GetWordDataList().wordDatas[questionID]);
        }
    }

    private void MainMenu()
    {
        askQuestionPage.SetActive(false);
        mainMenuPage.SetActive(true);
    }

    void OnFilesSelectedImage(string filePath)
    {
        byte[] bytes = File.ReadAllBytes(filePath);
        SetImageFromBytes(bytes);
    }

    public void SetImageFromBytes(byte[] bytes)
    {
        // Texture oluþtur ve byte dizisinden veriyi yükle
        Texture2D texture = new Texture2D(2, 2);
        texture.LoadImage(bytes);

        // Texture'tan Sprite oluþtur
        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one * 0.5f);

        // Image'a atama
        wordReferanceImage.sprite = sprite;
    }
}
