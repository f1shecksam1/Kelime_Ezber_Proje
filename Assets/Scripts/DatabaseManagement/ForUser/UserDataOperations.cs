using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserDataOperations
{
    private UserDataList userDataList;

    public UserDataOperations()
    {
        userDataList = UserDataList.LoadUserDataList();
    }

    public UserDataList GetUserDataList()
    {
        return this.userDataList;
    }

    public bool CreatAndAddUserToList(string username, string password, string email)
    {
        if (!IsUserExist(username, password, email))
        {
            UserData userData = new UserData(this.userDataList.userDatas.Count + 1, username, password, email);
            this.userDataList.userDatas.Add(userData);
            UserDataList.SaveUserDataList(userDataList);
            GameManager.Instance.activeUser = userData;
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool IsUserExist(string username, string password, string email)
    {
        foreach (UserData user in userDataList.userDatas)
        {
            if (user.username == username || user.password == password || user.email == email)
            {
                return true;
            }
        }
        return false;
    }

    public bool IsUserExist(string username, string email)
    {
        foreach (UserData user in userDataList.userDatas)
        {
            if (user.username == username || user.email == email)
            {
                return true;
            }
        }
        return false;
    }

    public UserData AuthenticateUser(string username, string password)
    {
        foreach (UserData user in userDataList.userDatas)
        {
            Debug.Log(user.username);
            Debug.Log(user.password);
            Debug.Log(username);
            Debug.Log(password);
            if (user.username == username && user.password == password)
            {
                Debug.Log("sa");
                return user;
            }
        }
        return null;
    }

    public bool ChangeUserPassword(string username, string email, string newPassword)
    {
        if (IsUserExist(username, email))
        {
            Debug.Log("sa");
            foreach (UserData user in userDataList.userDatas)
            {
                if (user.username == username && user.email == email)
                {
                    user.password = newPassword;
                    UserDataList.SaveUserDataList(userDataList);
                    return true;
                }
            }
        }
        return false;

    }

    public void AddWordToKnowedWordDictionaryFirst(UserData activeUser, int knowedWordID, DateTime knowTime)
    {
        activeUser.firstKnowWord.Add(knowedWordID, knowTime);
        UserDataList.SaveUserDataList(userDataList);
    }

    public void AddWordToKnowedWordDictionarySecond(UserData activeUser, int knowedWordID, DateTime knowTime)
    {
        activeUser.secondKnowWord.Add(knowedWordID, knowTime);
        activeUser.firstKnowWord.Remove(knowedWordID);
        UserDataList.SaveUserDataList(userDataList);
    }

    public void AddWordToKnowedWordDictionaryThird(UserData activeUser, int knowedWordID, DateTime knowTime)
    {
        activeUser.thirdKnowWord.Add(knowedWordID, knowTime);
        activeUser.secondKnowWord.Remove(knowedWordID);
        UserDataList.SaveUserDataList(userDataList);
    }

    public void AddWordToKnowedWordDictionaryFourth(UserData activeUser, int knowedWordID, DateTime knowTime)
    {
        activeUser.fourthKnowWord.Add(knowedWordID, knowTime);
        activeUser.thirdKnowWord.Remove(knowedWordID);
        UserDataList.SaveUserDataList(userDataList);
    }

    public void AddWordToKnowedWordDictionaryFifth(UserData activeUser, int knowedWordID, DateTime knowTime)
    {
        activeUser.fifthKnowWord.Add(knowedWordID, knowTime);
        activeUser.fourthKnowWord.Remove(knowedWordID);
        UserDataList.SaveUserDataList(userDataList);
    }

    public void RemoveWordToKnowedWordDictionarys(UserData activeUser, int knowedWordID)
    {
        if (activeUser.firstKnowWord.ContainsKey(knowedWordID))
        {
            activeUser.firstKnowWord.Remove(knowedWordID);
        }
        else if (activeUser.secondKnowWord.ContainsKey(knowedWordID))
        {
            activeUser.secondKnowWord.Remove(knowedWordID);
        }
        else if (activeUser.thirdKnowWord.ContainsKey(knowedWordID))
        {
            activeUser.thirdKnowWord.Remove(knowedWordID);
        }
        else if (activeUser.fourthKnowWord.ContainsKey(knowedWordID))
        {
            activeUser.fourthKnowWord.Remove(knowedWordID);
        }
        else if (activeUser.fifthKnowWord.ContainsKey(knowedWordID))
        {
            activeUser.fifthKnowWord.Remove(knowedWordID);
        }
        UserDataList.SaveUserDataList(userDataList);
    }

    public int[] SelectRandomWord(WordDataList wordDataList, UserData activeUser, int randomWordCount)
    {
        int[] selectedWordIDs = new int[randomWordCount];
        for (int i = 0; i < randomWordCount; i++)
        {
            int randomWordID = UnityEngine.Random.Range(0, wordDataList.wordDatas.Count);
            if (activeUser.firstKnowWord.ContainsKey(randomWordID))
            {
                i--;
            }
            else if (activeUser.secondKnowWord.ContainsKey(randomWordID))
            {
                i--;
            }
            else if (activeUser.thirdKnowWord.ContainsKey(randomWordID))
            {
                i--;
            }
            else if (activeUser.fourthKnowWord.ContainsKey(randomWordID))
            {
                i--;
            }
            else if (activeUser.fifthKnowWord.ContainsKey(randomWordID))
            {
                i--;
            }
            else
            {
                selectedWordIDs[i] = randomWordID;
            }
        }
        return selectedWordIDs;
    }

    public List<int> SelectAlreadyKnowedWordID(WordDataList wordDataList, UserData activeUser)
    {
        List<int> knowedWordIDs = new List<int>();
        foreach (DateTime values in activeUser.firstKnowWord.Values)
        {
            if ((DateTime.Now - values).TotalDays > 1)
            {
                knowedWordIDs.Add(FindKeyByValue(activeUser.firstKnowWord, values));
            }
        }
        foreach (DateTime values in activeUser.secondKnowWord.Values)
        {
            if ((DateTime.Now - values).TotalDays > 7)
            {
                knowedWordIDs.Add(FindKeyByValue(activeUser.secondKnowWord, values));
            }
        }
        foreach (DateTime values in activeUser.thirdKnowWord.Values)
        {
            if ((DateTime.Now - values).TotalDays > 30)
            {
                knowedWordIDs.Add(FindKeyByValue(activeUser.thirdKnowWord, values));
            }
        }
        foreach (DateTime values in activeUser.fourthKnowWord.Values)
        {
            if ((DateTime.Now - values).TotalDays > 90)
            {
                knowedWordIDs.Add(FindKeyByValue(activeUser.fourthKnowWord, values));
            }
        }
        foreach (DateTime values in activeUser.fifthKnowWord.Values)
        {
            if ((DateTime.Now - values).TotalDays > 180)
            {
                knowedWordIDs.Add(FindKeyByValue(activeUser.fifthKnowWord, values));
            }
        }
        return knowedWordIDs;
    }

    private int FindKeyByValue(Dictionary<int, DateTime> dictionary, DateTime value)
    {
        foreach (var kvp in dictionary)
        {
            if (kvp.Value.Equals(value))
            {
                return kvp.Key;
            }
        }
        return -1;
    }
}
