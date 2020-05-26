using UnityEngine.UI;

namespace K_UnityGF
{
    /// <summary>
    /// 控件-选择框
    /// </summary>
    public class UIToggle : UIWidgetBase<Toggle>
    {
        /// <summary>
        /// 
        /// </summary>
        protected override void OnStartAction()
        {
            base.OnStartAction();
            widget.onValueChanged.AddListener(ValueChangeEvent);
        }

        /// <summary>
        /// 值变事件
        /// </summary>
        /// <param name="_bool"></param>
        protected virtual void ValueChangeEvent(bool _bool) { }
    }
}

