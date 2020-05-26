using UnityEngine;
using LitJson;
using System.IO;
using System.Collections.Generic;

/// <summary>
/// Json数据读取
/// </summary>
public class JsonRead : MonoBehaviour
{
    private string filePath;

    private void Start()
    {
        filePath = Application.dataPath + "/K_UnityGF/DataReading/Json/Test.json";
        GetJsonInfo();
    }

    private void GetJsonInfo()
    {
        if (File.Exists(filePath))
        {
            StreamReader streamReader = new StreamReader(filePath);
            string str = streamReader.ReadToEnd();
            JsonInfo jsonInfo = JsonMapper.ToObject<JsonInfo>(str);
            Debug.Log(jsonInfo.ServerUrl);
            Debug.Log(jsonInfo.ConnectState);
            Debug.Log(jsonInfo.UserInfo[0].Name);
            Debug.Log(jsonInfo.UserData.Age);
        }
    }

    public class JsonInfo
    {
        public string ServerUrl;
        public bool ConnectState;
        public List<UserInfo> UserInfo;
        public UserData UserData;
    }

    public class UserData
    {
        public string Name;
        public int Age;
    }

    public class UserInfo
    {
        public int Id;
        public string Name;
    }
}

