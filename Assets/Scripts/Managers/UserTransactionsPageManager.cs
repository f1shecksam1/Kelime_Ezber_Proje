using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserTransactionsPageManager : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button registerButton;
    [SerializeField] private Button loginButton;
    [SerializeField] private Button exitButton;

    [Header("Pages")]
    [SerializeField] private GameObject userTransactionsPage;
    [SerializeField] private GameObject registerPage;
    [SerializeField] private GameObject loginPage;

    private void Start()
    {
        registerPage.SetActive(false);
        loginPage.SetActive(false);
        userTransactionsPage.SetActive(true);

        registerButton.onClick.AddListener(OpenRegisterPage);
        loginButton.onClick.AddListener(OpenLoginPage);
        exitButton.onClick.AddListener(ExitGame);
    }

    private void OpenRegisterPage()
    {
        registerPage.SetActive(true);
        userTransactionsPage.SetActive(false);
    }

    private void OpenLoginPage()
    {
        loginPage.SetActive(true);
        userTransactionsPage.SetActive(false);
    }

    private void ExitGame()
    {
        Application.Quit();
    }


}
