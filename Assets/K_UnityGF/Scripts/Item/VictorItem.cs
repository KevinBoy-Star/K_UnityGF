using UnityEngine;
using HighlightingSystem;
using System;
using System.Collections;

namespace K_UnityGF
{
    /// <summary>
    /// 三维物体基类(功能:动画/高亮/射线(进入、点击、离开)/步骤操作)
    /// </summary>
    public class VictorItem : ItemBase
    {
        /// <summary>
        /// 动画控制器
        /// </summary>
        protected Animator _animator;

        /// <summary>
        /// 
        /// </summary>
        protected Collider _collider;       //碰撞器

        /// <summary>
        /// 边缘高亮控制器
        /// </summary>
        protected Highlighter _hightlighter;

        /// <summary>
        /// 物体步骤id索引值
        /// </summary>
        [Header("步骤ID")]
        public int stepID;

        /// <summary>
        /// 
        /// </summary>
        protected override void InitOperation()
        {
            base.InitOperation();
            _animator = GetComponentByType<Animator>();
            _collider = GetComponentByType<Collider>();
            if (GetComponent<Highlighter>() == null)
            {
                _hightlighter = gameObject.AddComponent<Highlighter>();
            }
            else
            {
                _hightlighter = GetComponent<Highlighter>();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        protected override void OnStartAction()
        {
            base.OnStartAction();
            Delegation.Event_RayEnter += Event_RayEnter;
            Delegation.Event_ClickItem += Event_ClickItem;
            Delegation.Event_RayExit += Event_RayExit;
            Delegation.Event_StartStep += StartStep;
        }

        /// <summary>
        /// 
        /// </summary>
        protected override void OnDestroyAction()
        {
            base.OnDestroyAction();
            Delegation.Event_RayEnter -= Event_RayEnter;
            Delegation.Event_ClickItem -= Event_ClickItem;
            Delegation.Event_RayExit -= Event_RayExit;
            Delegation.Event_StartStep -= StartStep;
        }

        private void OnMouseEnter()
        {
            if (!CCU.gameState.Equals(GameState.GameStart)) return;
            RayEnter();
        }

        private void OnMouseExit()
        {
            if (!CCU.gameState.Equals(GameState.GameStart)) return;
            RayExit();
        }

        private void OnMouseDown()
        {
            if (!CCU.gameState.Equals(GameState.GameStart)) return;
            if (stepID.Equals(CCU.stepIndex))
            {
                ClickItem_StepEqual();
            }
            else
            {
                ClickItem_StepUnEqual();
            }
        }

        #region[闪烁]

        /// <summary>
        /// 开始闪烁
        /// </summary>
        public void StartLight() { _hightlighter.ConstantOn(); }

        /// <summary>
        /// 开始闪烁 添加回调
        /// </summary>
        public void StartLight(Action callBack)
        {
            _hightlighter.ConstantOn();
            callBack();
        }

        /// <summary>
        /// 停止闪烁
        /// </summary>
        public void StopLight() { _hightlighter.ConstantOff(); }

        /// <summary>
        /// 停止闪烁 添加回调
        /// </summary>
        public void StopLight(Action callBack)
        {
            _hightlighter.ConstantOff();
            callBack();
        }

        #endregion

        #region[动画]

        /// <summary>
        /// 更新动画状态(暂停/播放)
        /// </summary>
        /// <param name="_animState">动画状态  暂停 播放</param>
        public void UpdateAnimState(AnimState _animState)
        {
            try
            {
                switch (_animState)
                {
                    case AnimState.Play:
                        _animator.speed = 1;
                        break;
                    case AnimState.Pause:
                        _animator.speed = 0;
                        break;
                }
            }
            catch
            {
                Debug.LogError("动画控制器为空");
            }
        }

        /// <summary>
        /// 动画暂停
        /// </summary>
        /// <param name="delayTime"></param>
        /// <param name="callBack"></param>
        public void AnimPause(float delayTime, Action callBack)
        {
            _animator.speed = 1;
            StartCoroutine(DelayPause(delayTime, callBack));
        }

        /// <summary>
        /// 延迟暂停
        /// </summary>
        /// <param name="DelayTime"></param>
        /// <param name="callBack"></param>
        /// <returns></returns>
        IEnumerator DelayPause(float DelayTime, Action callBack)
        {
            yield return new WaitForSeconds(DelayTime);
            _animator.speed = 0;
            callBack();
        }

        /// <summary>
        /// 播放动画 Trigger
        /// </summary>
        /// <param name="_param">动画播放条件</param>
        public void PlayAnim(string _param)
        {
            try
            {
                _animator.SetTrigger(_param);
            }
            catch
            {
                Debug.LogError("动画控制器为空");
            }
        }

        /// <summary>
        /// 播放动画 带动画完成播放后回调参数
        /// </summary>
        /// <param name="_param"></param>
        /// <param name="delayTime"></param>
        /// <param name="callBack"></param>
        public void PlayAnim(string _param, float delayTime, Action callBack)
        {
            try
            {
                _animator.SetTrigger(_param);
                StartCoroutine(DelayAnimEvent(delayTime, callBack));
            }
            catch
            {
                Debug.LogError("动画控制器为空");
            }
        }

        /// <summary>
        /// 延迟执行动画完成事件
        /// </summary>
        /// <param name="delayTime"></param>
        /// <param name="callBack"></param>
        /// <returns></returns>
        IEnumerator DelayAnimEvent(float delayTime, Action callBack)
        {
            yield return new WaitForSeconds(delayTime);
            callBack();
        }

        /// <summary>
        /// 播放动画 bool
        /// </summary>
        /// <param name="_param"></param>
        /// <param name="_value"></param>
        public void PlayAnim(string _param, bool _value)
        {
            try
            {
                _animator.SetBool(_param, _value);
            }
            catch
            {
                Debug.LogError("动画控制器为空");
            }
        }

        /// <summary>
        /// 播放动画 int
        /// </summary>
        /// <param name="_param"></param>
        /// <param name="_value"></param>
        public void PlayAnim(string _param, int _value)
        {
            try
            {
                _animator.SetInteger(_param, _value);
            }
            catch
            {
                Debug.LogError("动画控制器为空");
            }
        }

        /// <summary>
        /// 播放动画 float
        /// </summary>
        /// <param name="_param"></param>
        /// <param name="_value"></param>
        public void PlayAnim(string _param, float _value)
        {
            try
            {
                _animator.SetFloat(_param, _value);
            }
            catch
            {
                Debug.LogError("动画控制器为空");
            }
        }

        /// <summary>
        /// 动画控制器显隐
        /// </summary>
        /// <param name="_shState">显隐状态 true 显示 false 隐藏</param>
        public void AnimatorShowHidden(bool _shState)
        {
            try
            {
                _animator.enabled = _shState;
            }
            catch
            {
                Debug.LogError("动画控制器为空");
            }
        }

        #endregion

        #region[VR]

        /// <summary>
        /// 事件 射线进入
        /// </summary>
        /// <param name="_selectTransform">选择得物体</param>
        private void Event_RayEnter(Transform _selectTransform)
        {
            if (transform.Equals(_selectTransform))
            {
                RayEnter();
            }
        }

        /// <summary>
        /// 事件 点击物体
        /// </summary>
        /// <param name="_selectTransform">选择得物体</param>
        private void Event_ClickItem(Transform _selectTransform)
        {
            if (!CCU.gameState.Equals(GameState.GameStart)) return;
            if (!transform.Equals(_selectTransform)) return;
            if (stepID.Equals(CCU.stepIndex))
            {
                ClickItem_StepEqual();
            }
            else
            {
                ClickItem_StepUnEqual();
            }
        }

        /// <summary>
        /// 事件 射线离开
        /// </summary>
        /// <param name="_selectTransform">选择得物体</param>
        private void Event_RayExit(Transform _selectTransform)
        {
            if (transform.Equals(_selectTransform))
            {
                RayExit();
            }
        }

        #endregion

        #region[步骤]

        /// <summary>
        /// 开始步骤
        /// </summary>
        /// <param name="_stepIndex">步骤索引</param>
        private void StartStep()
        {
            if (stepID.Equals(CCU.stepIndex))
            {
                StartStepEvent();
            }
        }

        /// <summary>
        /// 开始步骤事件
        /// </summary>
        protected virtual void StartStepEvent() { }

        /// <summary>
        /// 更新步骤
        /// </summary>
        public void UpdateStep()
        {
            CCU.stepIndex = stepID + 1;
            CCU.stepCount++;
        }

        #endregion

        #region[交互]

        /// <summary>
        /// 射线进入
        /// </summary>
        protected virtual void RayEnter() { }

        /// <summary>
        /// 点击物体 步骤正确
        /// </summary>
        protected virtual void ClickItem_StepEqual() { }

        /// <summary>
        /// 点击物体 步骤错误
        /// </summary>
        protected virtual void ClickItem_StepUnEqual() { }

        /// <summary>
        /// 射线离开
        /// </summary>
        protected virtual void RayExit() { }

        #endregion
    }
}

