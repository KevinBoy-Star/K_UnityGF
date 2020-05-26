using UnityEngine;

namespace K_UnityGF
{
    /// <summary>
    /// 游戏物体基类
    /// </summary>
    public abstract class ObjectBase : MonoBehaviour
    {

        private void Awake()
        {
            InitOperation();
        }

        private void Start() { OnStartAction(); }

        private void OnDestroy() { OnDestroyAction(); }

        /// <summary>
        /// Awake 初始化操作
        /// </summary>
        protected abstract void InitOperation();

        /// <summary>
        /// Start 初始化操作
        /// </summary>
        protected abstract void OnStartAction();

        /// <summary>
        /// 销毁操作
        /// </summary>
        protected abstract void OnDestroyAction();
    }
}

