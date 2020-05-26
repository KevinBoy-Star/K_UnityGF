using System.Collections.Generic;
using K_UnityGF;
using UnityEngine.UI;
using LitJson;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LoginManager : UIManagerBase
{
    public Text hintText;
    public InputField user, password;
    private string inputUser, inputPassword;

    private string url = "http://192.168.0.199:8090/member/login/vr-login";
    private Dictionary<string, string> dic = new Dictionary<string, string>();

    protected override void OnStartAction()
    {
        base.OnStartAction();
        Delegation.Event_MessageServer += Event_MessageServer;
    }

    protected override void OnDestroyAction()
    {
        base.OnDestroyAction();
        Delegation.Event_MessageServer -= Event_MessageServer;
    }

    private void OnEnable()
    {
        hintText.text = "";
    }

    public override void Login()
    {
        dic.Clear();
        inputUser = user.text;
        inputPassword = password.text;
        dic.Add("workNum", inputUser);
        dic.Add("password", inputPassword);
        StartCoroutine(NetworkUtil.PostDatas(url, dic, true));
    }

    private void Event_MessageServer(string _content)
    {
        JsonData jsonData = JsonMapper.ToObject(_content);
        int code = (int)jsonData["code"];
        if (code.Equals(1))
        {
            SceneManager.LoadScene("Game");
        }
        else
        {
            hintText.text = "用户名或密码错误";
        }
    }
}
