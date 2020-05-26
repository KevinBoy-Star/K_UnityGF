using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

namespace K_UnityGF
{
    /// <summary>
    /// 字体工具 一键更换场景内所有字体
    /// </summary>
    public class FontWindow : EditorWindow
    {
        /// <summary>
        /// 
        /// </summary>
        [MenuItem("Tools/更换字体")]
        public static void Open()
        {
            EditorWindow.GetWindow(typeof(FontWindow));
        }

        Font toChange;
        static Font toChangeFont;
        FontStyle toFontStyle;
        static FontStyle toChangeFontStyle;

        void OnGUI()
        {
            toChange = (Font)EditorGUILayout.ObjectField(toChange, typeof(Font), true, GUILayout.MinWidth(100f));
            toChangeFont = toChange;
            toFontStyle = (FontStyle)EditorGUILayout.EnumPopup(toFontStyle, GUILayout.MinWidth(100f));
            toChangeFontStyle = toFontStyle;
            if (GUILayout.Button("更换"))
            {
                Change();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static void Change()
        {
            //寻找Hierarchy面板下所有的Text
            var tArray = Resources.FindObjectsOfTypeAll(typeof(Text));
            for (int i = 0; i < tArray.Length; i++)
            {
                Text t = tArray[i] as Text;
                Undo.RecordObject(t, t.gameObject.name);
                t.font = toChangeFont;
                t.fontStyle = toChangeFontStyle;
                EditorUtility.SetDirty(t);
            }
            Debug.Log("Succed");
        }
    }
}
