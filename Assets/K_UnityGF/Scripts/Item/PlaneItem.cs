using UnityEngine.EventSystems;

namespace K_UnityGF
{
    /// <summary>
    /// 二维物体基类 功能:鼠标进入、点击、移出
    /// </summary>
    public class PlaneItem : ItemBase, IPointerEnterHandler,
        IPointerExitHandler, IPointerClickHandler
    {
        /// <summary>
        /// 鼠标点击
        /// </summary>
        /// <param name="eventData"></param>
        public virtual void OnPointerClick(PointerEventData eventData) { }

        /// <summary>
        /// 鼠标进入
        /// </summary>
        /// <param name="eventData"></param>
        public virtual void OnPointerEnter(PointerEventData eventData) { }

        /// <summary>
        /// 鼠标移出
        /// </summary>
        /// <param name="eventData"></param>
        public virtual void OnPointerExit(PointerEventData eventData) { }
    }
}


