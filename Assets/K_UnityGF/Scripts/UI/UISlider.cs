using UnityEngine.UI;

namespace K_UnityGF
{
    /// <summary>
    /// 控件-滑动条
    /// </summary>
    public class UISlider : UIWidgetBase<Slider>
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
        /// 滑动条值变事件
        /// </summary>
        /// <param name="_value"></param>
        protected virtual void ValueChangeEvent(float _value) { }
    }
}

