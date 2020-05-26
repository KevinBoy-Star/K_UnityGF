using System;
using UnityEngine;

namespace K_UnityGF
{
    /// <summary>
    /// 物体抓取基类
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class GrabItem : VictorItem
    {
        /// <summary>
        /// 
        /// </summary>
        protected Rigidbody _rig;           //刚体
        private bool initKinematic;         //初始化物理状态
        private bool initTrigger;           //初始化碰撞器状态
        private bool initColliderEnable;    //初始化碰撞器显隐
        private Transform originParent;     //初始父物体
        private Vector3 originPosition;     //初始位置
        private Vector3 originEuler;        //初始旋转
        private Vector3 originScale;        //初始缩放

        /// <summary>
        /// 
        /// </summary>
        [Header("拾取后是否重置位置")]
        public bool _grabAfterReset;

        [Header("拾取后需要重置位置的信息")]
        /// <summary>
        /// 
        /// </summary>
        public GrabInfo grabInfo;           //拾取信息

        /// <summary>
        /// 
        /// </summary>
        protected override void InitOperation()
        {
            base.InitOperation();
            _rig = GetComponentByType<Rigidbody>();
        }

        /// <summary>
        /// 
        /// </summary>
        protected override void OnStartAction()
        {
            base.OnStartAction();
            Delegation.Event_GrabStart += Event_GrabStart;
            Delegation.Event_GrabEnd += Event_GrabEnd;
            InitInfo();
        }

        /// <summary>
        /// 
        /// </summary>
        protected override void OnDestroyAction()
        {
            base.OnDestroyAction();
            Delegation.Event_GrabStart -= Event_GrabStart;
            Delegation.Event_GrabEnd -= Event_GrabEnd;
        }

        private void OnTriggerEnter(Collider other)
        {
            GrabDetection(other, grabInfo.detectionTargetName);
        }

        #region[Grab detection]

        /// <summary>
        /// 拾取检测
        /// </summary>
        private void GrabDetection(Collider other, string detectionName)
        {
            if (!CCU.gameState.Equals(GameState.GameStart)) return;
            if (!other.transform.name.Equals(detectionName)) return;
            if (stepID.Equals(CCU.stepIndex))
            {
                GrabDetection_Equal(other);
            }
            else
            {
                GrabDetection_UnEqual(other);
            }
        }

        /// <summary>
        /// 拾取检测成功
        /// </summary>
        protected virtual void GrabDetection_Equal(Collider other) { }

        /// <summary>
        /// 拾取检测失败
        /// </summary>
        protected virtual void GrabDetection_UnEqual(Collider other) { }

        #endregion

        #region[Start grab]

        /// <summary>
        /// 开始拾取
        /// </summary>
        /// <param name="_parent"></param>
        /// <param name="_selectTransform"></param>
        private void Event_GrabStart(Transform _parent, Transform _selectTransform)
        {
            if (!CCU.gameState.Equals(GameState.GameStart)) return;
            if (!transform.Equals(_selectTransform)) return;
            if (stepID.Equals(CCU.stepIndex))
            {
                GrabStart_StepEqual(_parent);
            }
            else
            {
                GrabStart_StepUnEqual();
            }
        }

        /// <summary>
        /// 开始拾取 步骤正确
        /// </summary>
        /// <param name="_parent"></param>
        protected virtual void GrabStart_StepEqual(Transform _parent)
        {
            _rig.isKinematic = true;
            _collider.isTrigger = true;
            transform.parent = _parent;
            if (_grabAfterReset)
            {
                transform.localPosition = grabInfo.grabResetPosition;
                transform.localEulerAngles = grabInfo.grabResetEuler;
            }
            StopLight();
        }

        /// <summary>
        /// 开始拾取 步骤错误
        /// </summary>
        protected virtual void GrabStart_StepUnEqual() { }

        #endregion

        #region[End grab]

        /// <summary>
        /// 事件 结束拾取
        /// </summary>
        /// <param name="_selectTransform"></param>
        private void Event_GrabEnd(Transform _selectTransform)
        {
            if (!CCU.gameState.Equals(GameState.GameStart)) return;
            if (!transform.Equals(_selectTransform)) return;
            if (stepID.Equals(CCU.stepIndex))
            {
                GrabEnd();
            }
        }

        /// <summary>
        /// 结束抓取
        /// </summary>
        protected virtual void GrabEnd() { }

        #endregion

        #region[Item info]

        /// <summary>
        /// 初始化信息
        /// </summary>
        public void InitInfo()
        {
            initKinematic = _rig.isKinematic;
            initTrigger = _collider.isTrigger;
            initColliderEnable = _collider.enabled;
            originParent = transform.parent;
            originPosition = transform.position;
            originEuler = transform.eulerAngles;
            originScale = transform.localScale;
        }

        /// <summary>
        /// 重置信息
        /// </summary>
        public void ResetInfo()
        {
            _rig.isKinematic = initKinematic;
            _collider.isTrigger = initTrigger;
            _collider.enabled = initColliderEnable;
            transform.parent = originParent;
            transform.position = originPosition;
            transform.eulerAngles = originEuler;
            transform.localScale = originScale;
            AddResetInfoEvent();
        }

        /// <summary>
        /// 添加重置信息事件
        /// </summary>
        protected virtual void AddResetInfoEvent() { }

        #endregion

        /// <summary>
        /// 结构体 拾取后的物体信息
        /// </summary>
        [Serializable]
        public struct GrabInfo
        {
            /// <summary>
            /// 
            /// </summary>
            [Header("拾取后的位置")]
            public Vector3 grabResetPosition;

            /// <summary>
            /// 
            /// </summary>
            [Header("拾取后的旋转")]
            public Vector3 grabResetEuler;

            /// <summary>
            /// 
            /// </summary>
            [Header("被抓取时所要检测的对象名称")]
            public string detectionTargetName;
        }
    }
}

