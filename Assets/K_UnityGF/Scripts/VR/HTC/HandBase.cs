using UnityEngine;

namespace K_UnityGF
{
    /// <summary>
    /// 手柄基类(功能:获取手柄输入)
    /// </summary>
    public class HandBase : MonoBehaviour
    {
        private SteamVR_TrackedObject trackedObject;       //事件控制器
        protected SteamVR_Controller.Device device;        

        private void Awake()
        {
            InitOperation();
        }

        protected virtual void Start() { }

        protected virtual void OnDestroy() { }

        protected virtual void Update() { }

        /// <summary>
        /// 初始化
        /// </summary>
        protected virtual void InitOperation()
        {
            trackedObject = transform.GetComponentInParent<SteamVR_TrackedObject>();
        }

        /// <summary>
        /// 获取控制器
        /// </summary>
        protected void GetDevice()
        {
            device = SteamVR_Controller.Input((int)trackedObject.index);
        }
    }
}
