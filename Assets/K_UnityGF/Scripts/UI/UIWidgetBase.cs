using UnityEngine;

namespace K_UnityGF
{
    /// <summary>
    /// UI控件基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class UIWidgetBase<T> : ObjectBase where T : Component
    {
        /// <summary>
        /// UI控件基类
        /// </summary>
        protected T widget;

        /// <summary>
        /// UI控制器
        /// </summary>
        protected UIManagerBase uiManager;

        /// <summary>
        /// 
        /// </summary>
        protected override void InitOperation()
        {
            widget = GetComponent<T>();
            if (GetComponentInParent<UIManagerBase>() == null)
            {
                uiManager = transform.GetComponentInParent<Canvas>().transform.gameObject.AddComponent<UIManagerBase>();
            }
            else
            {
                uiManager = GetComponentInParent<UIManagerBase>();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        protected override void OnStartAction() { }

        /// <summary>
        /// 
        /// </summary>
        protected override void OnDestroyAction() { }
    }
}

