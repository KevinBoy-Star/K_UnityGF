using UnityEngine;

namespace K_UnityGF
{
    /// <summary>
    /// 物体基类(功能:显示/隐藏)
    /// </summary>
    public class ItemBase : ObjectBase
    {
        /// <summary>
        /// 游戏运行后显示
        /// </summary>
        [Header("游戏运行后是否显示")]
        public bool runningDisplay = true;

        /// <summary>
        /// 
        /// </summary>
        protected override void InitOperation()
        {
            if (!CCU.ItemDic.ContainsKey(transform.name))
            {
                CCU.ItemDic.Add(transform.name, gameObject);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        protected override void OnStartAction() { if (runningDisplay == false) { gameObject.SetActive(false); } }

        /// <summary>
        /// 
        /// </summary>
        protected override void OnDestroyAction() { }

        /// <summary>
        /// 根据指定类型获取组件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected T GetComponentByType<T>() where T : Component
        {
            T _T;
            if (GetComponent<T>() != null)
            {
                _T = GetComponent<T>();
            }
            else
            {
                _T = null;
            }
            return _T;
        }
    }
}

