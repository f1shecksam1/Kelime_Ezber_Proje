using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;

public class MainMenuPanelManager : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button playButton;
    [SerializeField] private Button exitGameButton;
    [SerializeField] private Button logoutButton;
    [SerializeField] private Button settingPanelButton;
    [SerializeField] private Button settingPanelExitButton;
    [SerializeField] private Button increaseRandomQuestionCountButton;
    [SerializeField] private Button decreaseRandomQuestionCountButton;
    [SerializeField] private Button userAnalysisPanelButton;
    [SerializeField] private Button userAnalysisPanelExitButton;
    [SerializeField] private Button userAnalysisCreateFileButton;

    [Header("Pages")]
    [SerializeField] private GameObject mainMenuPage;
    [SerializeField] private GameObject askQuestionPage;
    [SerializeField] private GameObject userTransactionsPage;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject userAnalysisPanel;

    [Header("Text")]
    [SerializeField] private TMP_Text userAnalysisUserNameText;
    [SerializeField] private TMP_Text randomQuestionCountText;
    [SerializeField] private TMP_Text firsKnowedWordCountText;
    [SerializeField] private TMP_Text secondKnowedWordCountText;
    [SerializeField] private TMP_Text thirdKnowedWordCountText;
    [SerializeField] private TMP_Text fourthKnowedWordCountText;
    [SerializeField] private TMP_Text fifthKnowedWordCountText;
    [SerializeField] private TMP_Text sixthKnowedWordCountText;

    private void Start()
    {
        randomQuestionCountText.text = GameManager.Instance.randomQuestionCount.ToString();
        settingsPanel.SetActive(false);
        mainMenuPage.SetActive(false);
        playButton.onClick.AddListener(Play);
        exitGameButton.onClick.AddListener(ExitGame);
        logoutButton.onClick.AddListener(Logout);
        settingPanelButton.onClick.AddListener(SettingPanel);
        settingPanelExitButton.onClick.AddListener(SettingPanelExit);
        increaseRandomQuestionCountButton.onClick.AddListener(IncreaseRandomQuestionCount);
        decreaseRandomQuestionCountButton.onClick.AddListener(DecreaseRandomQuestionCount);
        userAnalysisPanelButton.onClick.AddListener(UserAnalysisPanel);
        userAnalysisPanelExitButton.onClick.AddListener(UserAnalysisPanelExit);
        userAnalysisCreateFileButton.onClick.AddListener(UserAnalysisCreateFile);
    }

    private void Play()
    {
        if ((DateTime.Now - GameManager.Instance.activeUser.lastAppOpenDate).TotalDays > 1)
        {
            GameManager.Instance.randomWordIDs = GameManager.Instance.userDataOperations.SelectRandomWord(GameManager.Instance.wordDataOperations.GetWordDataList(), GameManager.Instance.activeUser, GameManager.Instance.randomQuestionCount);
            GameManager.Instance.alreadyKnowedWordIDsList = GameManager.Instance.userDataOperations.SelectAlreadyKnowedWordID(GameManager.Instance.wordDataOperations.GetWordDataList(), GameManager.Instance.activeUser).ToArray();
            GameManager.Instance.activeUser.lastAppOpenDate = DateTime.Now;
            UserDataList.SaveUserDataList(GameManager.Instance.userDataOperations.GetUserDataList());
            mainMenuPage.SetActive(false);
            askQuestionPage.SetActive(true);
        }
        else
        {
            Debug.Log("KullaniciBugunGirisYapti");
        }
    }

    private void ExitGame()
    {
        Application.Quit();
    }

    private void Logout()
    {
        settingsPanel.SetActive(false);
        userAnalysisPanel.SetActive(false);
        GameManager.Instance.activeUser = null;
        mainMenuPage.SetActive(false);
        userTransactionsPage.SetActive(true);
    }

    private void SettingPanel()
    {
        if (!settingsPanel.activeSelf)
        {
            settingsPanel.SetActive(true);
            randomQuestionCountText.text = GameManager.Instance.randomQuestionCount.ToString();
        }
    }

    private void SettingPanelExit()
    {
        if (settingsPanel.activeSelf)
        {
            settingsPanel.SetActive(false);
        }
    }

    private void IncreaseRandomQuestionCount()
    {
        GameManager.Instance.randomQuestionCount++;
        randomQuestionCountText.text = GameManager.Instance.randomQuestionCount.ToString();
    }

    private void DecreaseRandomQuestionCount()
    {
        GameManager.Instance.randomQuestionCount--;
        randomQuestionCountText.text = GameManager.Instance.randomQuestionCount.ToString();
    }

    private void UserAnalysisPanel()
    {
        if (!userAnalysisPanel.activeSelf)
        {
            int totalWords = GameManager.Instance.wordDataOperations.GetWordDataList().wordDatas.Count;
            int firstKnowWorldCount = GameManager.Instance.activeUser.firstKnowWord.Count;
            int secondKnowWordCount = GameManager.Instance.activeUser.secondKnowWord.Count;
            int thirdKnowWordCount = GameManager.Instance.activeUser.thirdKnowWord.Count;
            int fourthKnowWordCount = GameManager.Instance.activeUser.fourthKnowWord.Count;
            int fifthKnowWordCount = GameManager.Instance.activeUser.fifthKnowWord.Count;

            firsKnowedWordCountText.text = firstKnowWorldCount.ToString();
            secondKnowedWordCountText.text = secondKnowWordCount.ToString();
            thirdKnowedWordCountText.text = thirdKnowWordCount.ToString();
            fourthKnowedWordCountText.text = fourthKnowWordCount.ToString();
            fifthKnowedWordCountText.text = fifthKnowWordCount.ToString();
            userAnalysisPanel.SetActive(true);
        }
    }

    private void UserAnalysisPanelExit()
    {
        if (userAnalysisPanel.activeSelf)
        {
            userAnalysisPanel.SetActive(false);
        }
    }

    private void UserAnalysisCreateFile()
    {
        Debug.Log("sa");

        // Sayýmlarý al
        int firstKnowWorldCount = GameManager.Instance.activeUser.firstKnowWord.Count;
        int secondKnowWordCount = GameManager.Instance.activeUser.secondKnowWord.Count;
        int thirdKnowWordCount = GameManager.Instance.activeUser.thirdKnowWord.Count;
        int fourthKnowWordCount = GameManager.Instance.activeUser.fourthKnowWord.Count;
        int fifthKnowWordCount = GameManager.Instance.activeUser.fifthKnowWord.Count;

        // Uygulama veri dizinini al
        string appDataPath = Application.persistentDataPath;

        // PDF dosyasýnýn yolu
        string pdfPath = Path.Combine(appDataPath, "WordCounts.pdf");

        // PDF dokümanýný oluþtur
        using (FileStream fs = new FileStream(pdfPath, FileMode.Create, FileAccess.Write, FileShare.None))
        {
            Document document = new Document();
            PdfWriter writer = PdfWriter.GetInstance(document, fs);
            document.Open();

            // Sayýmlarý PDF'ye yaz
            document.Add(new Paragraph("Bir kere bilinen kelimeler sayýsý: " + firstKnowWorldCount));
            document.Add(new Paragraph("Iki kere bilinen kelimeler sayýsý: " + secondKnowWordCount));
            document.Add(new Paragraph("Uc kere bilinen kelimeler sayýsý: " + thirdKnowWordCount));
            document.Add(new Paragraph("Dort kere bilinen kelimeler sayýsý: " + fourthKnowWordCount));
            document.Add(new Paragraph("Bes kere bilinen kelimeler sayýsý: " + fifthKnowWordCount));

            document.Close();
            writer.Close();
        }

        Console.WriteLine("PDF dosyasý oluþturuldu: " + pdfPath);
    }

}

