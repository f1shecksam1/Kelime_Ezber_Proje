using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
            randomQuestionCountText.text = GameManager.Instance.randomQuestionCount.ToString();
            settingsPanel.SetActive(true);
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

            firsKnowedWordCountText.text = GameManager.Instance.activeUser.firstKnowWord.Count.ToString() + "/" + (totalWords - (secondKnowWordCount + thirdKnowWordCount + fourthKnowWordCount + fifthKnowWordCount)).ToString();
            secondKnowedWordCountText.text = secondKnowWordCount.ToString() + "/" + (firstKnowWorldCount + thirdKnowWordCount + fourthKnowWordCount + fifthKnowWordCount).ToString();
            thirdKnowedWordCountText.text = thirdKnowWordCount.ToString() + "/" + (firstKnowWorldCount + secondKnowWordCount + fourthKnowWordCount + fifthKnowWordCount).ToString();
            fourthKnowedWordCountText.text = fourthKnowWordCount.ToString() + "/" + (firstKnowWorldCount + thirdKnowWordCount + secondKnowWordCount + fifthKnowWordCount).ToString();
            fifthKnowedWordCountText.text = fifthKnowWordCount.ToString() + "/" + (firstKnowWorldCount + thirdKnowWordCount + fourthKnowWordCount + secondKnowWordCount).ToString();
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
    }
}
