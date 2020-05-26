using UnityEngine;

namespace K_UnityGF
{
    /// <summary>
    /// 动画事件基类 Event1-15适用于一段动画多个事件 如流水线动画等
    /// </summary>
    public abstract class AnimEventBase : MonoBehaviour
    {
        /// <summary>
        /// 事件1
        /// </summary>
        protected virtual void AnimEvent1() { CommonOperation(); }

        /// <summary>
        /// 事件2
        /// </summary>
        protected virtual void AnimEvent2() { CommonOperation(); }

        /// <summary>
        /// 事件3
        /// </summary>
        protected virtual void AnimEvent3() { CommonOperation(); }

        /// <summary>
        /// 事件4
        /// </summary>
        protected virtual void AnimEvent4() { CommonOperation(); }

        /// <summary>
        /// 事件5
        /// </summary>
        protected virtual void AnimEvent5() { CommonOperation(); }

        /// <summary>
        /// 事件6
        /// </summary>
        protected virtual void AnimEvent6() { CommonOperation(); }

        /// <summary>
        /// 事件7
        /// </summary>
        protected virtual void AnimEvent7() { CommonOperation(); }

        /// <summary>
        /// 事件8
        /// </summary>
        protected virtual void AnimEvent8() { CommonOperation(); }

        /// <summary>
        /// 事件9
        /// </summary>
        protected virtual void AnimEvent9() { CommonOperation(); }

        /// <summary>
        /// 事件10
        /// </summary>
        protected virtual void AnimEvent10() { CommonOperation(); }

        /// <summary>
        /// 事件11
        /// </summary>
        protected virtual void AnimEvent11() { CommonOperation(); }

        /// <summary>
        /// 事件12
        /// </summary>
        protected virtual void AnimEvent12() { CommonOperation(); }

        /// <summary>
        /// 事件13
        /// </summary>
        protected virtual void AnimEvent13() { CommonOperation(); }

        /// <summary>
        /// 事件14
        /// </summary>
        protected virtual void AnimEvent14() { CommonOperation(); }

        /// <summary>
        /// 事件15
        /// </summary>
        protected virtual void AnimEvent15() { CommonOperation(); }

        /// <summary>
        /// 动画共同事件操作
        /// </summary>
        protected virtual void CommonOperation() { }

        /// <summary>
        /// 动画播放完成事件
        /// </summary>
        protected abstract void AnimFinish();
    }
}

