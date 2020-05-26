using UnityEngine;

namespace K_UnityGF
{
    /// <summary>
    /// 游戏控制器基类
    /// </summary>
    public class GameManagerBase : ObjectBase
    {
        /// <summary>
        /// 
        /// </summary>
        protected override void InitOperation() { }

        /// <summary>
        /// 
        /// </summary>
        protected override void OnStartAction()
        {
            CCU.stepIndex = 0;
            CCU.stepCount = 0;

            Delegation.Event_TaskSuccess += TaskSuccess;
            Delegation.Event_TaskFail += TaskFail;
        }

        /// <summary>
        /// 
        /// </summary>
        protected override void OnDestroyAction()
        {
            Delegation.Event_TaskSuccess -= TaskSuccess;
            Delegation.Event_TaskFail -= TaskFail;
            CCU.ClearAllDic();
        }

        /// <summary>
        /// 
        /// </summary>
        protected virtual void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape)) { Util.ExitGame(); }
            if (Input.GetKeyDown(KeyCode.Return)) { CCU.gameState = GameState.GameStart; }
        }

        /// <summary>
        /// 任务成功
        /// </summary>
        protected virtual void TaskSuccess() { }

        /// <summary>
        /// 任务失败
        /// </summary>
        protected virtual void TaskFail()
        {
            CCU.gameState = GameState.GameOver;
        }
    }
}

