using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;

public class CreateLayouts : MonoBehaviour
{
    public GameObject uiElementPrefab;
    private int count;

    private void Start()
    {
        LoadElementName();
        count = jsonInfo.elements.Count;
        CreateElement(count);
    }

    private void CreateElement(int _elementCount)
    {
        while(count>0)
        {
            GameObject uiElement = Instantiate(uiElementPrefab, transform, false);
            Element element = uiElement.GetComponent<Element>();
            Elements m_element = jsonInfo.elements[jsonInfo.elements.Count - count];
            element.text_Name.text = m_element.name;
            element.text_Age.text = m_element.age.ToString();
            element.text_Sex.text = m_element.sex;

            count--;
        }
    }

    private void LoadElementName()
    {
        string url = Application.streamingAssetsPath + "/Server.json";
        StreamReader stream = new StreamReader(url);
        jsonInfo = JsonMapper.ToObject<JsonInfo>(stream.ReadToEnd());
    }

    private JsonInfo jsonInfo;

    public class JsonInfo
    {
        public List<Elements> elements;
    }

    public struct Elements
    {
        public string name;
        public int age;
        public string sex;
    }
}
