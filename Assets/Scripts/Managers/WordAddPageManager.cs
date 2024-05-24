using SimpleFileBrowser;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class WordAddPageManager : MonoBehaviour
{
    [Header("ForInPageTransition")]
    [SerializeField] private TMP_InputField wordEnglishIF;
    [SerializeField] private TMP_InputField wordTurkishIF;
    [SerializeField] private TMP_InputField wordSentenceExamplesIF;
    [SerializeField] private Button addPronunciationToWord;
    [SerializeField] private Button addReferanceImageToWord;
    [SerializeField] private Button playSound;
    [SerializeField] private Button addWordToDataBase;
    [SerializeField] private Button previusPage;
    [SerializeField] private Image wordReferanceImage;

    [Header("ForPageTransition")]
    [SerializeField] private GameObject wordAddPagePanel;
    [SerializeField] private GameObject adminPagePanel;

    AudioSource audioSource;
    private string pronunciationPath;
    private string imagePath;


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        playSound.onClick.AddListener(PlaySound);
        addPronunciationToWord.onClick.AddListener(AddPronunciationToWord);
        addReferanceImageToWord.onClick.AddListener(AddReferanceImageToWord);
        addWordToDataBase.onClick.AddListener(AddWordToDatabase);
        previusPage.onClick.AddListener(PreviusPage);
    }

    private void PlaySound()
    {
        if (!string.IsNullOrEmpty(pronunciationPath))
        {
            StartCoroutine(LoadMusic(pronunciationPath));
        }
    }

    private IEnumerator LoadMusic(string filePath)
    {
        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip("file:///" + filePath, AudioType.MPEG))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error loading music: " + www.error);
            }
            else
            {
                AudioClip clip = DownloadHandlerAudioClip.GetContent(www);
                audioSource.clip = clip;
                audioSource.Play();
            }
        }
    }

    private void AddPronunciationToWord() 
    {
        FileBrowser.AddQuickLink("Users", "C:\\Users", null);
        StartCoroutine(ShowLoadDialogCoroutineAudio());

    }

    IEnumerator ShowLoadDialogCoroutineAudio()
    {
        yield return FileBrowser.WaitForLoadDialog(FileBrowser.PickMode.Files, true, null, null, "Select Files", "Load");
        Debug.Log(FileBrowser.Success);

        if (FileBrowser.Success)
            OnFilesSelectedAudio(FileBrowser.Result);
    }

    void OnFilesSelectedAudio(string[] filePath)
    {
        pronunciationPath = filePath[0];
    }

    private void AddReferanceImageToWord()
    {
        FileBrowser.AddQuickLink("Users", "C:\\Users", null);
        StartCoroutine(ShowLoadDialogCoroutineImage());

    }

    IEnumerator ShowLoadDialogCoroutineImage()
    {
        yield return FileBrowser.WaitForLoadDialog(FileBrowser.PickMode.Files, true, null, null, "Select Files", "Load");
        Debug.Log(FileBrowser.Success);

        if (FileBrowser.Success)
            OnFilesSelectedImage(FileBrowser.Result);
    }

    void OnFilesSelectedImage(string[] filePath)
    {
        imagePath = filePath[0];
        byte[] bytes = File.ReadAllBytes(imagePath);
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

    public void AddWordToDatabase()
    {
        if (wordEnglishIF != null && wordTurkishIF != null && wordSentenceExamplesIF != null &&imagePath != null && pronunciationPath != null)
        {
            GameManager.Instance.wordDataOperations.CreateAndAddWordToList(wordEnglishIF.text, wordTurkishIF.text, wordSentenceExamplesIF.text, pronunciationPath, imagePath);
            wordEnglishIF.text = null;
            wordTurkishIF.text = null;
            wordSentenceExamplesIF.text = null;
            pronunciationPath = null;
            imagePath = null;
            wordReferanceImage.sprite = null;
            audioSource.Stop();
            Debug.Log("sa");
        }
    }

    public void PreviusPage()
    {
        wordAddPagePanel.SetActive(false);
        adminPagePanel.SetActive(true);
    }
}
