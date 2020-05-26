using UnityEngine;

namespace K_UnityGF
{
    /// <summary>
    /// UI控制器基类 功能:通知显示某个面板、任务成功、任务失败
    /// </summary>
    public class UIManagerBase : ObjectBase
    {
        /// <summary>
        /// 
        /// </summary>
        protected override void InitOperation()
        {
            InitGameManager();
            InitPanelsDic();
        }

        /// <summary>
        /// 初始化游戏控制器
        /// </summary>
        private void InitGameManager()
        {
            try
            {
                if (FindObjectOfType<GameManagerBase>() == null)
                {
                    GameObject gameObject = new GameObject("GameManager");
                    gameObject.AddComponent<GameManagerBase>();
                }
            }
            catch
            {

            }
        }

        /// <summary>
        /// 
        /// </summary>
        protected override void OnStartAction()
        {
            Delegation.Event_MessagePanel += Event_MessagePanel;
            Event_MessagePanel(0);
        }

        /// <summary>
        /// 初始化界面字典
        /// </summary>
        private void InitPanelsDic()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                if (!CCU.PanelsDic.ContainsKey(transform.GetChild(i).name))
                {
                    CCU.PanelsDic.Add(transform.GetChild(i).name, transform.GetChild(i).gameObject);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        protected override void OnDestroyAction()
        {
            Delegation.Event_MessagePanel -= Event_MessagePanel;
        }

        /// <summary>
        /// 通知界面
        /// </summary>
        /// <param name="panelID"></param>
        public void Event_MessagePanel(int panelID)
        {
            try
            {
                for (int i = 0; i < transform.childCount; i++)
                {
                    if (i.Equals(panelID))
                    {
                        transform.GetChild(i).gameObject.SetActive(true);
                    }
                    else
                    {
                        transform.GetChild(i).gameObject.SetActive(false);
                    }
                }
            }
            catch
            {
                Debug.LogError("面板索引值超出");
            }
        }

        /// <summary>
        /// 显示界面
        /// </summary>
        /// <param name="panelID"></param>
        public void ShowPanel(int panelID)
        {
            try
            {
                transform.GetChild(panelID).gameObject.SetActive(true);
            }
            catch
            {
                Debug.LogError("面板索引值超出");
            }
        }

        /// <summary>
        /// 显示界面
        /// </summary>
        /// <param name="panelName"></param>
        public void ShowPanel(string panelName)
        {
            try
            {
                transform.Find(panelName).gameObject.SetActive(true);
            }
            catch
            {
                Debug.LogError("此名称面板不存在");
            }
        }

        /// <summary>
        /// 隐藏界面
        /// </summary>
        /// <param name="panelID"></param>
        public void HidePanel(int panelID)
        {
            try
            {
                transform.GetChild(panelID).gameObject.SetActive(false);
            }
            catch
            {
                Debug.LogError("面板索引值超出");
            }
        }

        /// <summary>
        /// 隐藏界面
        /// </summary>
        /// <param name="panelName"></param>
        public void HidePanel(string panelName)
        {
            try
            {
                transform.Find(panelName).gameObject.SetActive(false);
            }
            catch
            {
                Debug.LogError("此名称面板不存在");
            }
        }

        /// <summary>
        /// 开始游戏
        /// </summary>
        public virtual void StartGame() { CCU.gameState = GameState.GameStart; }

        /// <summary>
        /// 登录
        /// </summary>
        public virtual void Login() { }
    }
}

