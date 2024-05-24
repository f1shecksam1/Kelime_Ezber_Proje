using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ForgotPasswordPageManager : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button resetPasswordButton;
    [SerializeField] private Button previusPageButton;

    [Header("InputFields")]
    [SerializeField] private TMP_InputField mailIF;
    [SerializeField] private TMP_InputField userNameIF;
    [SerializeField] private TMP_InputField newPasswordIF;

    [Header("Pages")]
    [SerializeField] private GameObject loginPage;
    [SerializeField] private GameObject forgotPasswordPage;

    private void Start()
    {
        resetPasswordButton.onClick.AddListener(ResetPassword);
        previusPageButton.onClick.AddListener(PreviusPage);
    }

    private void ResetPassword()
    {
        Debug.Log("ResetPassword");
        if (GameManager.Instance.userDataOperations.ChangeUserPassword(userNameIF.text, mailIF.text, newPasswordIF.text))
        {
            forgotPasswordPage.SetActive(false);
            loginPage.SetActive(true);
        }
    }

    private void PreviusPage()
    {
        forgotPasswordPage.SetActive(false);
        loginPage.SetActive(true);
    }
}
