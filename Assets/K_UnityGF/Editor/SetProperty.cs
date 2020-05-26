using UnityEditor;

namespace K_UnityGF
{
    /// <summary>
    /// UIButton 脚本字段设置
    /// </summary>
    [CustomEditor(typeof(UIButton))]
    public class Property_Button : Editor
    {
        private SerializedObject serialized;
        private SerializedProperty eventType, panelID, sceneName;

        private void OnEnable()
        {
            serialized = new SerializedObject(target);
            eventType = serialized.FindProperty("eventType");
            panelID = serialized.FindProperty("panelID");
            sceneName = serialized.FindProperty("sceneName");
        }

        /// <summary>
        /// 
        /// </summary>
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            serialized.Update();

            EditorGUILayout.PropertyField(eventType);

            switch (eventType.enumValueIndex)
            {
                case 3:
                    EditorGUILayout.PropertyField(panelID);
                    break;
                case 4:
                    EditorGUILayout.PropertyField(sceneName);
                    break;
            }
            serialized.ApplyModifiedProperties();
        }
    }
}
