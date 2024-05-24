using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RegisterPageManager : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button registerButton;
    [SerializeField] private Button previusPageButton;

    [Header("InputFields")]
    [SerializeField] private TMP_InputField mailIF;
    [SerializeField] private TMP_InputField userNameIF;
    [SerializeField] private TMP_InputField passwordIF;

    [Header("Pages")]
    [SerializeField] private GameObject registerPage;
    [SerializeField] private GameObject userTransactionsPage;
    [SerializeField] private GameObject mainMenuPage;

    private void Start()
    {
        previusPageButton.onClick.AddListener(PreviusPage);
        registerButton.onClick.AddListener(Register);
    }

    private void PreviusPage()
    {
        registerPage.SetActive(false);
        userTransactionsPage.SetActive(true);
    }

    private void Register()
    {
        if (userNameIF.text != null && mailIF.text != null && passwordIF.text != null && GameManager.Instance.userDataOperations.CreatAndAddUserToList(userNameIF.text, passwordIF.text, mailIF.text))
        {
            registerPage.SetActive(false);
            mainMenuPage.SetActive(true);
            userNameIF.text = null;
            passwordIF.text = null;
            mailIF.text = null;
        }
    }
}
