using UnityEngine;
using UnityEngine.UI;

namespace K_UnityGF
{
    /// <summary>
    /// 控件-按钮
    /// </summary>
    public class UIButton : UIWidgetBase<Button>
    {
        /// <summary>
        /// 事件类型
        /// </summary>
        private enum EventType
        {
            Null,               //空
            Login,              //登录
            StartGame,          //开始游戏
            MessagePanel,       //通知面板
            LoadScene,          //加载场景
            ExitGame            //退出游戏
        }

        /// <summary>
        /// 
        /// </summary>
        protected override void OnStartAction()
        {
            base.OnStartAction();
            widget.onClick.AddListener(ButtonEvent);
        }

        /// <summary>
        /// 按钮事件
        /// </summary>
        private void ButtonEvent()
        {
            switch (eventType)
            {
                case EventType.MessagePanel:
                    uiManager.Event_MessagePanel(panelID);
                    break;
                case EventType.LoadScene:
                    Util.LoadScene(sceneName);
                    break;
                case EventType.ExitGame:
                    Util.ExitGame();
                    break;
                case EventType.StartGame:
                    uiManager.StartGame();
                    break;
                case EventType.Login:
                    uiManager.Login();
                    break;
                default:
                    Debug.Log("<color=green>事件类型为空</color>");
                    break;
            }
        }

        #region

        [Header("事件类型")]
        [HideInInspector]
        [SerializeField]
        private EventType eventType;

        [Header("面板ID")]
        [HideInInspector]
        [SerializeField]
        private int panelID;

        [Header("场景名称")]
        [HideInInspector]
        [SerializeField]
        private string sceneName;

        #endregion
    }
}

