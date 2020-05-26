using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

/// <summary>
/// 小键盘控制器
/// </summary>
public class Keypad : MonoBehaviour
{
    private string StrValue;                            //写入的字符
    private List<string> Value = new List<string>();    //集合 储存写入的值

    public delegate void UpdateValue(string value);     //委托 提交数值
    public static event UpdateValue Event_UpdateValue;  //事件

    private void Start()
    {
        AddKeysEvent();
    }

    private void Update()
    {
        Event_UpdateValue?.Invoke(StrValue);
    }

    /// <summary>
    /// 添加所有按键事件
    /// </summary>
    private void AddKeysEvent()
    {
        foreach (Transform item in transform)
        {
            string KeyName = item.name;
            switch (KeyName)
            {
                case "0":
                case "1":
                case "2":
                case "3":
                case "4":
                case "5":
                case "6":
                case "7":
                case "8":
                case "9":
                    item.GetComponent<Button>().onClick.AddListener(delegate
                    {
                        StrValue = null;
                        Value.Add(KeyName);
                        StrValue = null;
                        for (int i = 0; i < Value.Count; i++)
                        {
                            StrValue = StrValue + Value[i];
                        }
                    });
                    break;
                case ".":
                    item.GetComponent<Button>().onClick.AddListener(OnPoint);
                    break;
                case "Backspace":
                    item.GetComponent<Button>().onClick.AddListener(Backspace);
                    break;
                case "Sure":
                    item.GetComponent<Button>().onClick.AddListener(delegate
                    {
                        transform.parent.gameObject.SetActive(false);
                    });
                    break;
                case "Clear":
                    item.GetComponent<Button>().onClick.AddListener(ClearContent);
                    break;
                default:
                    break;
            }
        }
    }

    private void OnPoint()
    {
        StrValue = null;
        if (!Value.Contains("."))
        {
            Value.Add(".");
        }
        for (int i = 0; i < Value.Count; i++)
        {
            StrValue = StrValue + Value[i];
        }
    }

    private void Backspace()
    {
        StrValue = null;
        if (Value.Count > 0)
        {
            Value.RemoveAt(Value.Count - 1);
        }
        for (int i = 0; i < Value.Count; i++)
        {
            StrValue = StrValue + Value[i];
        }
    }

    private void ClearContent()
    {
        StrValue = null;
        Value.Clear();
        for (int i = 0; i < Value.Count; i++)
        {
            StrValue = "";
        }
    }
}
