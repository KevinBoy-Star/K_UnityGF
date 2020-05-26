using UnityEngine;
using UnityEngine.UI;

namespace K_UnityGF
{
    /// <summary>
    /// 控件-文本
    /// </summary>
    public class UIText : UIWidgetBase<Text>
    {
        [Header("文本内容")]
        [SerializeField]
        private string[] contents;

        private string blank = "\u3000\u3000";

        private void OnEnable()
        {
            widget.text = "";
            UpdateContent();
        }

        /// <summary>
        /// 更新文本
        /// </summary>
        /// <param name="contents"></param>
        private void UpdateContent()
        {
            string content = "";
            for (int i = 0; i < contents.Length; i++)
            {
                content += blank + contents[i] + "\n";
            }
            widget.text = content;
        }
    }
}

