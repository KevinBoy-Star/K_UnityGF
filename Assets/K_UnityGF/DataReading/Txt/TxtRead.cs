using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class TxtRead : MonoBehaviour
{
    private string url;

    private void Awake()
    {
        url = Application.dataPath + "/K_UnityGF/DataReading/Txt/Server.txt";
    }

    private void Start()
    {
        LoadTxt();
    }

    private void LoadTxt()
    {
        string line;
        StreamReader sr = new StreamReader(url);
        while((line=sr.ReadLine())!=null)
        {
            Debug.Log(line.ToString());
        }
    }
}
