using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public UserData adminUser;

    private void Awake()
    {
        DontDestroyOnLoad(this);

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public WordDataOperations wordDataOperations;
    public UserDataOperations userDataOperations;
    public UserData activeUser;
    public int randomQuestionCount = 10;
    public int[] randomWordIDs;
    public int[] alreadyKnowedWordIDsList;

    void Start()
    {
        adminUser = new UserData(-1, "Sami", "Sami1", "sami@gmail.com");
        wordDataOperations = new WordDataOperations();
        userDataOperations = new UserDataOperations();
    }
}
