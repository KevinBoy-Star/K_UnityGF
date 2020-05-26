using System.Collections.Generic;
using UnityEngine;

namespace K_UnityGF
{
    /// <summary>
    /// 中央控制器
    /// </summary>
    public static class CCU
    {
        /// <summary>
        /// 游戏模式
        /// </summary>
        [HideInInspector]
        public static GameMode gameMode;

        /// <summary>
        /// 游戏状态
        /// </summary>
        [HideInInspector]
        public static GameState gameState;

        /// <summary>
        /// 场景名称
        /// </summary>
        [HideInInspector]
        public static string sceneName;

        /// <summary>
        /// 步骤计数器
        /// </summary>
        [HideInInspector]
        public static int stepIndex = 0;

        /// <summary>
        /// 步骤数量
        /// </summary>
        [HideInInspector]
        public static int stepCount = 0;

        /// <summary>
        /// 更新步骤
        /// </summary>
        public static void UpdateStep()
        {
            stepIndex++;
            stepCount++;
        }

        /// <summary>
        /// 游戏物体字典
        /// </summary>
        public static Dictionary<string, GameObject> ItemDic = new Dictionary<string, GameObject>();

        /// <summary>
        /// UI界面字典 
        /// </summary>
        public static Dictionary<string, GameObject> PanelsDic = new Dictionary<string, GameObject>();

        /// <summary>
        /// 批处理显示物体
        /// </summary>
        /// <param name="itemNameArray">物体名称数组</param>
        public static void BatchDisplayItem(string[] itemNameArray)
        {
            foreach (string itemName in itemNameArray)
            {
                if (ItemDic.ContainsKey(itemName))
                {
                    ItemDic[itemName].SetActive(true);
                }
            }
        }

        /// <summary>
        /// 批处理隐藏物体
        /// </summary>
        /// <param name="itemNameArray">物体名称数组</param>
        public static void BatchHiddenItem(string[] itemNameArray)
        {
            foreach (string itemName in itemNameArray)
            {
                if (ItemDic.ContainsKey(itemName))
                {
                    ItemDic[itemName].SetActive(false);
                }
            }
        }

        /// <summary>
        /// 清空所有字典
        /// </summary>
        public static void ClearAllDic()
        {
            ItemDic.Clear();
            PanelsDic.Clear();
        }

        /// <summary>
        /// 从游戏物体中获取指定组件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="_itemName"></param>
        /// <returns></returns>
        public static T GetComponentByItemDic<T>(string _itemName) where T : Component
        {
            T _T;

            if (ItemDic.ContainsKey(_itemName))
            {
                _T = ItemDic[_itemName].GetComponent<T>();
            }
            else
            {
                _T = null;
            }

            return _T;
        }
    }
}
