using UnityEngine;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;

public class XmlRead : MonoBehaviour
{
    private string filePath;

    private void Awake()
    {
        filePath = Application.dataPath + "/K_UnityGF/DataReading/Xml/XmlInfo.xml";
    }

    private void Start()
    {
        WriteXml();
    }

    /// <summary>
    /// xml文件写入
    /// </summary>
    private void WriteXml()
    {
        ServerConfig server = new ServerConfig();
        server.students.Add(new Student("黄飞龙",25,3));
        server.students.Add(new Student("张玲", 22, 1));
        FileInfo info = new FileInfo(filePath);
        StreamWriter sw;
        if(!info.Exists)
        {
            sw = info.CreateText();
        }
        else
        {
            info.Delete();
            sw = info.CreateText();
        }
        XmlSerializer serializer = new XmlSerializer(typeof(ServerConfig));
        serializer.Serialize(sw, server);
        sw.Close();
    }

    /// <summary>
    /// 读取xml文件
    /// </summary>
    private void LoadXml()
    {
        FileStream fs = new FileStream(filePath, FileMode.Open);
        XmlSerializer serializer = new XmlSerializer(typeof(ServerConfig));
        ServerConfig server = (ServerConfig)serializer.Deserialize(fs);
        Debug.Log(server.students[0].name);
    }

    public class ServerConfig
    {
        public List<Student> students = new List<Student>();
    }

    public class Student
    {
        public Student() { }

        public Student(string _name, int _age,int _classes)
        {
            name = _name;
            age = _age;
            classes = _classes;
        }

        public string name;
        public int age;
        public int classes;
    }
}
