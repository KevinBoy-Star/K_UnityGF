using UnityEngine.UI;

namespace K_UnityGF
{
    /// <summary>
    /// 控件-输入框
    /// </summary>
    public class UIInputField : UIWidgetBase<InputField>
    {
        /// <summary>
        /// 
        /// </summary>
        protected override void OnStartAction()
        {
            base.OnStartAction();
            widget.onValueChanged.AddListener(ValueChangeEvent);
            widget.onEndEdit.AddListener(EndEvent);
        }

        /// <summary>
        /// 输入时执行事件
        /// </summary>
        /// <param name="_content"></param>
        protected virtual void ValueChangeEvent(string _content) { }

        /// <summary>
        /// 输入完成后执行事件
        /// </summary>
        /// <param name="_content"></param>
        protected virtual void EndEvent(string _content) { }
    }
}

