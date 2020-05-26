using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LoadABByServer : MonoBehaviour
{
    private AssetBundle assetBundle;
    private UnityWebRequest request;
    private string url;

    private void Awake()
    {
        url = "http://192.168.0.116/AssetBundles/";
    }

    private void Start()
    {
        StartCoroutine(LoadBucketByServer("bucket"));
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            StartCoroutine(LoadBucketByServer("player"));
        }
    }

    private void LoadBucketByLocal()
    {
        assetBundle = AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/AssetBundles/bucket.unity3d");
        GameObject bucket = assetBundle.LoadAsset<GameObject>("bucket");
        Instantiate(bucket);
    }

    IEnumerator LoadBucketByServer(string abName)
    {
        request = UnityWebRequestAssetBundle.GetAssetBundle(url + abName + ".unity3d");
        yield return request.SendWebRequest();
        if (request.isHttpError || request.isNetworkError)
        {
            Debug.LogError("Load fail");
        }
        else
        {
            assetBundle = DownloadHandlerAssetBundle.GetContent(request);
            GameObject getPrefab = assetBundle.LoadAsset<GameObject>(abName);
            GameObject prefab = Instantiate(getPrefab);
            prefab.name = abName;
        }
        request.Dispose();
    }
}
