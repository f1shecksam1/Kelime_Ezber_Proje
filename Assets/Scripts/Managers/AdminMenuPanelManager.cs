using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdminMenuPanelManager : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button newWordAddButton;
    [SerializeField] private Button exitGameButton;
    [SerializeField] private Button logoutButton;
    [SerializeField] private Button settingPanelButton;
    [SerializeField] private Button settingPanelExitButton;

    [Header("Pages")]
    [SerializeField] private GameObject adminMenuPage;
    [SerializeField] private GameObject addQuestionPage;
    [SerializeField] private GameObject userTransactionsPage;
    [SerializeField] private GameObject settingsPanel;

    private void Start()
    {
        newWordAddButton.onClick.AddListener(NewWordAdd);
        exitGameButton.onClick.AddListener(ExitGame);
        logoutButton.onClick.AddListener(Logout);
        settingPanelButton.onClick.AddListener(SettingPanel);
        settingPanelExitButton.onClick.AddListener(SettingPanelExit);
    }

    private void NewWordAdd()
    {
        adminMenuPage.SetActive(false);
        addQuestionPage.SetActive(true);
    }

    private void ExitGame()
    {
        Application.Quit();
    }

    private void Logout()
    {
        settingsPanel.SetActive(false);
        GameManager.Instance.activeUser = null;
        adminMenuPage.SetActive(false);
        userTransactionsPage.SetActive(true);
    }

    private void SettingPanel()
    {
        if (!settingsPanel.activeSelf)
        {
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
}
