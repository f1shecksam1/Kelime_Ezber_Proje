using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class UserDataList
{
    public List<UserData> userDatas;

    // Constructor
    public UserDataList()
    {
        userDatas = new List<UserData>();
    }

    public static void SaveUserDataList(UserDataList userDataList)
    {
        string path = Application.persistentDataPath + "/" + "UserDataList" + ".json";
        string json = JsonConvert.SerializeObject(userDataList);
        File.WriteAllText(path, json);
        Debug.Log(Application.persistentDataPath);
    }

    public static UserDataList LoadUserDataList()
    {
        string path = Application.persistentDataPath + "/" + "UserDataList" + ".json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<UserDataList>(json);
        }
        else
        {
            Debug.LogWarning("Save file not found in " + path);
            UserDataList emptyUserDataList = new UserDataList();
            return emptyUserDataList;
        }
    }
}
