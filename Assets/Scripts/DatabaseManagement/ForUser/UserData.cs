using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class UserData
{
    public int userID;
    public string username;
    public string password;
    public string email;

    public DateTime lastAppOpenDate;

    public Dictionary<int, DateTime> firstKnowWord;
    public Dictionary<int, DateTime> secondKnowWord;
    public Dictionary<int, DateTime> thirdKnowWord;
    public Dictionary<int, DateTime> fourthKnowWord;
    public Dictionary<int, DateTime> fifthKnowWord;



    /**********************************************************************************************/

    public UserData(int userID, string username, string password, string email)
    {
        this.userID = userID;
        this.username = username;
        this.password = password;
        this.email = email;

        firstKnowWord = new Dictionary<int, DateTime>();
        secondKnowWord = new Dictionary<int, DateTime>();
        thirdKnowWord = new Dictionary<int, DateTime>();
        fourthKnowWord = new Dictionary<int, DateTime>();
        fifthKnowWord= new Dictionary<int, DateTime>();
    }
}
