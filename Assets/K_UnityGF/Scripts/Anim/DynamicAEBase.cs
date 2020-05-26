using UnityEngine;

namespace K_UnityGF
{
    /// <summary>
    /// 动态动画帧事件基类
    /// </summary>
    public abstract class DynamicAEBase : ObjectBase
    {
        private Animator _anim;                 //动画状态机
        private AnimationClip _clip;            //需要添加的动画

        protected override void OnStartAction()
        {
            _clip = _anim.runtimeAnimatorController.animationClips[0];
            AddAnimEvent();
        }

        protected override void InitOperation()
        {
            _anim = GetComponent<Animator>();
        }

        protected override void OnDestroyAction() { }

        /// <summary>
        /// 添加动画事件
        /// </summary>
        private void AddAnimEvent()
        {
            AnimationEvent event_Start = new AnimationEvent
            {
                functionName = "AnimStartEvent",
                time = 0
            };
            _clip.AddEvent(event_Start);


            AnimationEvent event_End = new AnimationEvent
            {
                functionName = "AnimEndEvent",
                time = _clip.length
            };
            _clip.AddEvent(event_End);
        }

        /// <summary>
        /// 动画开始事件
        /// </summary>
        protected virtual void AnimStartEvent() { }

        /// <summary>
        /// 动画播放完成事件
        /// </summary>
        protected abstract void AnimEndEvent();
    }
}

