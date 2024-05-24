using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoginPageManager : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button loginButton;
    [SerializeField] private Button previusPageButton;
    [SerializeField] private Button forgotPasswordButton;

    [Header("InputFields")]
    [SerializeField] private TMP_InputField userNameIF;
    [SerializeField] private TMP_InputField passwordIF;

    [Header("Pages")]
    [SerializeField] private GameObject loginPage;
    [SerializeField] private GameObject userTransactionsPage;
    [SerializeField] private GameObject forgotPasswordPage;
    [SerializeField] private GameObject mainMenuPage;
    [SerializeField] private GameObject adminPage;

    private void Start()
    {
        previusPageButton.onClick.AddListener(PreviusPage);
        loginButton.onClick.AddListener(Login);
        forgotPasswordButton.onClick.AddListener(ForgotPassword);
    }

    private void PreviusPage()
    {
        loginPage.SetActive(false);
        userTransactionsPage.SetActive(true);
    }

    private void Login()
    {
        Debug.Log("Login");
        Debug.Log(passwordIF.text);
        if (GameManager.Instance.userDataOperations.AuthenticateUser(userNameIF.text, passwordIF.text) != null)
        {
            Debug.Log("NotEmpty");
            GameManager.Instance.activeUser = GameManager.Instance.userDataOperations.AuthenticateUser(userNameIF.text, passwordIF.text);
            loginPage.SetActive(false);
            mainMenuPage.SetActive(true);
            userNameIF.text = null;
            passwordIF.text = null;
        }
        else if (GameManager.Instance.adminUser.username == userNameIF.text && GameManager.Instance.adminUser.password == passwordIF.text)
        {
            loginPage.SetActive(false);
            adminPage.SetActive(true);
            userNameIF.text = null;
            passwordIF.text = null;
        }
    }

    private void ForgotPassword()
    {
        loginPage.SetActive(false);
        forgotPasswordPage.SetActive(true);
    }
}
