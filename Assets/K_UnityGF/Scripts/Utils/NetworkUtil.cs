using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace K_UnityGF
{
    /// <summary>
    /// 后台信息交互类
    /// </summary>
    public sealed class NetworkUtil
    {
        private static UnityWebRequest request;             //Unity 网络协议
        private static string content;                      //从服务器获取的数据 json格式字符串
        private static byte[] bytes;                        //从服务器获取的字节数组
        private static List<IMultipartFormSection> iparams;

        /// <summary>
        /// HttpWebRequest Get请求
        /// </summary>
        /// <param name="_url"></param>
        /// <returns></returns>
        public static string HttpGet(string _url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(_url);
            request.Method = "GET";
            request.ContentType = "application/json";
            HttpWebResponse webResponse = (HttpWebResponse)request.GetResponse();
            StreamReader sr = new StreamReader(webResponse.GetResponseStream());
            return sr.ReadToEnd();
        }

        /// <summary>
        /// HttpWebRequest Post请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="dic"></param>
        /// <returns></returns>
        public static string HttpPost(string url, Dictionary<string, string> dic)
        {
            string result = "";
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";
            StringBuilder builder = new StringBuilder();
            int i = 0;
            foreach (var item in dic)
            {
                if (i > 0)
                    builder.Append("&");
                builder.AppendFormat("{0}={1}", item.Key, item.Value);
                i++;
            }
            byte[] data = Encoding.UTF8.GetBytes(builder.ToString());
            req.ContentLength = data.Length;
            using (Stream reqStream = req.GetRequestStream())
            {
                reqStream.Write(data, 0, data.Length);
                reqStream.Close();
            }
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            Stream stream = resp.GetResponseStream();
            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
            {
                result = reader.ReadToEnd();
            }
            return result;
        }

        /// <summary>
        /// Get请求 从服务器获取数据 获取之后通过委托发送
        /// </summary>
        /// <param name="_url">url 地址</param>
        /// <returns></returns>
        public static IEnumerator GetDatas(string _url)
        {
            request = UnityWebRequest.Get(_url);
            yield return request.SendWebRequest();
            if (request.error != null)
            {
                Debug.LogError(request.error);
            }
            else
            {
                content = request.downloadHandler.text;
                Delegation.Broadcast(Delegation.Event_MessageServer, content);
            }
        }

        /// <summary>
        /// Post请求 从服务器获取数据 获取之后通过委托发送 通过表单上传后返回json数据
        /// </summary>
        /// <param name="_url"></param>
        /// <param name="_dic"></param>
        /// <param name="_returnInfo"></param>
        /// <returns></returns>
        public static IEnumerator PostDatas(string _url, Dictionary<string, string> _dic, bool _returnInfo)
        {
            iparams = new List<IMultipartFormSection>();
            foreach (var item in _dic)
            {
                iparams.Add(new MultipartFormDataSection(item.Key, item.Value));
            }
            request = UnityWebRequest.Post(_url, iparams);
            yield return request.SendWebRequest();
            if (request.error != null)
            {
                Debug.LogError(request.error);
            }
            else
            {
                if (_returnInfo)
                {
                    content = request.downloadHandler.text;
                    Delegation.Broadcast(Delegation.Event_MessageServer, content);
                }
            }
        }

        /// <summary>
        /// 从服务器获取字节
        /// </summary>
        /// <param name="_url"></param>
        /// <param name="_dic"></param>
        /// <returns></returns>
        public static IEnumerator GetBytes(string _url, Dictionary<string, string> _dic)
        {
            iparams = new List<IMultipartFormSection>();
            foreach (var item in _dic)
            {
                iparams.Add(new MultipartFormDataSection(item.Key, item.Value));
            }
            request = UnityWebRequest.Post(_url, iparams);
            yield return request.SendWebRequest();
            if (request.error != null)
            {
                Debug.LogError(request.error);
            }
            else
            {
                bytes = request.downloadHandler.data;
                Delegation.Broadcast(Delegation.Event_LoadBytesByServer, bytes);
            }
        }

        /// <summary>
        /// 从服务器读取图片 
        /// </summary>
        /// <param name="_url"></param>
        /// <param name="textureWidth">图片的宽度</param>
        /// <param name="textureHeight">图片的高度</param>
        /// <returns></returns>
        public static IEnumerator LoadTexture(string _url, int textureWidth, int textureHeight)
        {
            request = UnityWebRequest.Get(_url);
            yield return request.SendWebRequest();
            if (request.error != null)
            {
                Debug.LogError("读取失败");
            }
            else
            {
                byte[] bytes = request.downloadHandler.data;
                Texture2D texture = new Texture2D(textureWidth, textureHeight);
                texture.LoadImage(bytes);
                Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one / 2);
                Delegation.Broadcast(Delegation.Event_LoadTextureByServer, sprite);
            }
        }
    }
}

