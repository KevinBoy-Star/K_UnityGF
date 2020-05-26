using UnityEngine;

namespace K_UnityGF
{
    /// <summary>
    /// 物体触碰基类
    /// </summary>
    public class TouchItem : VictorItem
    {
        /// <summary>
        /// 
        /// </summary>
        protected override void OnStartAction()
        {
            base.OnStartAction();
            Delegation.Event_TouchStart += Event_TouchStart;
        }

        /// <summary>
        /// 
        /// </summary>
        protected override void OnDestroyAction()
        {
            base.OnDestroyAction();
            Delegation.Event_TouchStart -= Event_TouchStart;
        }

        /// <summary>
        /// 开始触摸
        /// </summary>
        /// <param name="_selectTransform">所选择的对象</param>
        private void Event_TouchStart(Transform _selectTransform)
        {
            if (!CCU.gameState.Equals(GameState.GameStart)) return;
            if (!transform.Equals(_selectTransform)) return;
            if (stepID.Equals(CCU.stepIndex))
            {
                TouchStart_StepEqual();
            }
            else
            {
                TouchStart_StepUnEqual();
            }
        }

        /// <summary>
        /// 触碰后步骤正确执行事件
        /// </summary>
        protected virtual void TouchStart_StepEqual() { }

        /// <summary>
        /// 触碰后步骤错误执行事件
        /// </summary>
        protected virtual void TouchStart_StepUnEqual() { }
    }
}

