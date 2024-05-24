using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class AskQuestionPageManager : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button nextWordButton;
    [SerializeField] private Button wordApplyButton;
    [SerializeField] private Button playSoundButton;
    [SerializeField] private Button askFirstQuestionButton;

    [Header("Text")]
    [SerializeField] private TMP_Text wordTurkishText;
    [SerializeField] private TMP_Text wordExampleSentenceText;
    [SerializeField] private TMP_Text isInputCorretText;

    [Header("Pages")]
    [SerializeField] private GameObject askQuestionPage;
    [SerializeField] private GameObject mainMenuPage;
    [SerializeField] private GameObject askFirstQuestionGO;


    [SerializeField] private Image wordReferanceImage;
    [SerializeField] private TMP_InputField wordEnglishIF;

    int questionID;
    int questionCount;
    int alreadyKnowedQuestionCount;
    string wordSoundPath;

    private List<int> previusWordIDList;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        previusWordIDList = new List<int>();
        wordApplyButton.onClick.AddListener(WordApply);
        mainMenuButton.onClick.AddListener(MainMenu);
        nextWordButton.onClick.AddListener(NextWord);
        askFirstQuestionButton.onClick.AddListener(AskFirstQuestion);
        playSoundButton.onClick.AddListener(PlaySound);
    }

    private void AskFirstQuestion()
    {
        questionID = GameManager.Instance.randomWordIDs[questionCount];
        wordTurkishText.text = GameManager.Instance.wordDataOperations.GetWordDataList().wordDatas[questionID].wordTurkish;
        askFirstQuestionGO.SetActive(false);
    }

    private void WordApply()
    {
        if (wordEnglishIF.text != null)
        {
            if (GameManager.Instance.randomWordIDs.Length > questionCount)
            {
                questionID = GameManager.Instance.randomWordIDs[questionCount];
                previusWordIDList.Add(questionID);
                if (wordEnglishIF.text == GameManager.Instance.wordDataOperations.GetWordDataList().wordDatas[questionID].wordEnglish)
                {
                    Debug.Log("Kelime doðru bilindi");
                    GameManager.Instance.userDataOperations.AddWordToKnowedWordDictionary(GameManager.Instance.activeUser, questionID, DateTime.Now);
                    isInputCorretText.text = "Dogru";
                }
                else
                {
                    Debug.Log("Kelime yanlýþ bilindi");
                    GameManager.Instance.userDataOperations.RemoveWordToKnowedWordDictionarys(GameManager.Instance.activeUser, questionID);
                    isInputCorretText.text = "Yanlýþ";
                }
                wordTurkishText.text = GameManager.Instance.wordDataOperations.GetWordDataList().wordDatas[questionID].wordTurkish;
                wordExampleSentenceText.text = GameManager.Instance.wordDataOperations.GetWordDataList().wordDatas[questionID].wordSentenceExamples;
                OnFilesSelectedImage(WordData.GetWordImagePath(GameManager.Instance.wordDataOperations.GetWordDataList().wordDatas[questionID]));
                wordSoundPath = WordData.GetWordSoundPath(GameManager.Instance.wordDataOperations.GetWordDataList().wordDatas[questionID]);
            }
            else if (GameManager.Instance.alreadyKnowedWordIDsList.Length > questionCount - GameManager.Instance.randomWordIDs.Length)
            {
                questionID = GameManager.Instance.alreadyKnowedWordIDsList[alreadyKnowedQuestionCount - 1];
                previusWordIDList.Add(questionID);
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

    private void NextWord()
    {
        Debug.Log(GameManager.Instance.alreadyKnowedWordIDsList);
        if (previusWordIDList.Count < questionCount)
        {
            questionID = GameManager.Instance.randomWordIDs[questionCount];
            previusWordIDList.Add(questionID);
            wordEnglishIF.text = GameManager.Instance.wordDataOperations.GetWordDataList().wordDatas[questionID].wordEnglish;
            wordTurkishText.text = GameManager.Instance.wordDataOperations.GetWordDataList().wordDatas[questionID].wordTurkish;
            wordExampleSentenceText.text = GameManager.Instance.wordDataOperations.GetWordDataList().wordDatas[questionID].wordSentenceExamples;
            OnFilesSelectedImage(WordData.GetWordImagePath(GameManager.Instance.wordDataOperations.GetWordDataList().wordDatas[questionID]));
            wordSoundPath = WordData.GetWordSoundPath(GameManager.Instance.wordDataOperations.GetWordDataList().wordDatas[questionID]);
            Debug.Log("sa1");
        }
        else if (previusWordIDList.Count - 1 == questionCount && wordExampleSentenceText.text != null)
        {
            if (true)
            {
                questionCount++;
                if (questionCount < GameManager.Instance.randomWordIDs.Count()) // Ensure questionCount is within bounds
                {
                    questionID = GameManager.Instance.randomWordIDs[questionCount];
                    wordEnglishIF.text = null;
                    wordExampleSentenceText.text = null;
                    wordReferanceImage.sprite = null;
                    wordTurkishText.text = GameManager.Instance.wordDataOperations.GetWordDataList().wordDatas[questionID].wordTurkish;
                    isInputCorretText.text = null;
                    Debug.Log("sa2");
                }
                else
                {
                    Debug.LogWarning("questionCount exceeded the number of available questions.");
                    questionID = GameManager.Instance.alreadyKnowedWordIDsList[alreadyKnowedQuestionCount];
                    wordEnglishIF.text = null;
                    wordExampleSentenceText.text = null;
                    wordReferanceImage.sprite = null;
                    wordTurkishText.text = GameManager.Instance.wordDataOperations.GetWordDataList().wordDatas[questionID].wordTurkish;
                    isInputCorretText.text = null;
                    Debug.Log("sa2");
                    alreadyKnowedQuestionCount++;
                }
            }
            else
            {
                Debug.LogWarning("questionCount is already at the maximum.");
            }
        }
        audioSource.Stop();
    }

    private void PlaySound()
    {
        if (!string.IsNullOrEmpty(wordSoundPath))
        {
            Debug.Log("Loading audio from path: " + wordSoundPath);
            StartCoroutine(LoadMusic(wordSoundPath));
        }
        else
        {
            Debug.LogError("Word sound path is null or empty.");
        }
    }

    private IEnumerator LoadMusic(string filePath)
    {
        string fullPath = "file:///" + filePath;
        Debug.Log("Full audio path: " + fullPath);

        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip("file:///" + filePath, AudioType.MPEG))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error loading music: " + www.error + ", path: " + fullPath);
            }
            else
            {
                AudioClip clip = DownloadHandlerAudioClip.GetContent(www);
                if (clip != null)
                {
                    audioSource.clip = clip;
                    audioSource.Play();
                    Debug.Log("Audio loaded and playing.");
                }
                else
                {
                    Debug.LogError("Failed to load audio clip from path: " + fullPath);
                }
            }
        }
    }

}
