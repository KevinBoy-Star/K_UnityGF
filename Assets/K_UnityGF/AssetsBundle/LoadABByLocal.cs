using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 本地加载AB包
/// </summary>
public class LoadABByLocal : MonoBehaviour
{
    private readonly string abPath = Application.streamingAssetsPath + "/AssetBundles";
    private string modelPath;
    private string playerPath;
    private readonly string _floor = "Floor";
    private readonly string _player = "Player001";

    private AssetBundle assetBundle;

    private void Awake()
    {
        modelPath = abPath + "/models.unity3d";
        playerPath = abPath + "/player.unity3d";
    }

    private void Start()
    {
        InitItem(modelPath, _floor);
        InitItem(playerPath, _player);
    }

    /// <summary>
    /// 初始化游戏物体
    /// </summary>
    /// <param name="itemName"></param>
    private void InitItem(string typeName,string itemName)
    {
        GameObject prefab = Instantiate(GetItemByAssetBundle(typeName, itemName));
        prefab.name = itemName;
    }

    /// <summary>
    /// 从AB包获取某物体
    /// </summary>
    /// <param name="typeName"></param>
    /// <param name="fileName"></param>
    /// <returns></returns>
    private GameObject GetItemByAssetBundle(string typeName,string fileName)
    {
        GameObject prefab;
        assetBundle = AssetBundle.LoadFromFile(typeName);
        prefab = assetBundle.LoadAsset<GameObject>(fileName);
        return prefab;
    }
}
