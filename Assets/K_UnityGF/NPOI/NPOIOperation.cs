using UnityEngine;
using NPOI.XWPF.UserModel;
using System.IO;

namespace K_UnityGF
{
    /// <summary>
    /// NPOI 操作(与Word文档进行交互操作)
    /// </summary>
    public class NPOIOperation : MonoBehaviour
    {
        /// <summary>
        /// 文件路径
        /// </summary>
        private const string filePath = @"C:/Users/Administrator/Desktop";

        /// <summary>
        /// 文件名称
        /// </summary>
        private string fileName = "david.docx";

        private string path;

        private string tempText;

        /// <summary>
        /// word文档
        /// </summary>
        private XWPFDocument doc = new XWPFDocument();

        private void Start()
        {
            //path = Path.Combine(filePath, fileName);
            //fs = new FileStream(path, FileMode.Create, FileAccess.Write);

            //CreateParagraph(ParagraphAlignment.CENTER, 22, "#000000", "柴油机虚拟拆装实验指导书");
            //doc.Write(fs);
            //fs.Close();
            //fs.Dispose();

            ReadWriteFile();
        }

        /// <summary>
        /// 创建段落
        /// </summary>
        /// <param name="_alignment">对齐方式</param>
        /// <param name="_fontSize">字体大小</param>
        /// <param name="_color">字体颜色(16进制)</param>
        /// <param name="_content">内容</param>
        private void CreateParagraph(ParagraphAlignment _alignment, int _fontSize,
            string _color, string _content)
        {
            XWPFParagraph paragraph = doc.CreateParagraph();
            paragraph.Alignment = _alignment;
            XWPFRun run = paragraph.CreateRun();
            run.FontSize = _fontSize;
            run.SetColor(_color);
            run.FontFamily = "宋体";
            run.SetText(_content);
        }

        /// <summary>
        /// 读写文件
        /// </summary>
        private void ReadWriteFile()
        {
            path = Path.Combine(filePath, fileName);
            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            XWPFDocument doc = new XWPFDocument(fs);

            foreach (var para in doc.Paragraphs)
            {
                string oldText = para.ParagraphText;

                if (oldText == "")
                {
                    continue;
                }
                tempText = para.ParagraphText;

                #region[Replace]

                ReplaceData("{$Detection01}", "中锐");
                ReplaceData("{$Detection02}", "测试02");

                #endregion

                para.ReplaceText(oldText, tempText);
            }

            FileStream output = new FileStream(path, FileMode.Create);
            doc.Write(output);
            fs.Close();
            fs.Dispose();
            output.Close();
            output.Dispose();

            Debug.Log("<color=green>修改Word文档</color>");
        }

        /// <summary>
        /// 替换数据
        /// </summary>
        /// <param name="titleName"></param>
        /// <param name="replaceContent"></param>
        private void ReplaceData(string titleName, string replaceContent)
        {
            if (tempText.Contains(titleName))
            { tempText = tempText.Replace(titleName, replaceContent); }
        }
    }
}
