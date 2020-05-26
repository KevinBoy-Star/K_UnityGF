using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Element : MonoBehaviour
{
    internal Text text_Name, text_Age, text_Sex;

    private void Awake()
    {
        text_Name = transform.GetChild(0).GetComponent<Text>();
        text_Age = transform.GetChild(1).GetComponent<Text>();
        text_Sex = transform.GetChild(2).GetComponent<Text>();
    }
}
