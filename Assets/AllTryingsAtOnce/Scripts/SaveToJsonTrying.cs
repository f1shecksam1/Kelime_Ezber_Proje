using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;

public class SaveToJsonTrying : MonoBehaviour
{
    private UserDataList userDataList;

    private void Awake()
    {
        userDataList = UserDataList.LoadUserDataList();
        //userDataList.userDatas = UserDataList.LoadUserDatas("fileName");
    }

    private void Start()
    {
        UserData userData1 = new UserData(1, "s", "s", "s");
        UserData userData2 = new UserData(1, "s", "s", "s");
        userDataList.userDatas.Add(userData1);
        userDataList.userDatas.Add(userData2);
        UserDataList.SaveUserDataList(userDataList);
    }
}
