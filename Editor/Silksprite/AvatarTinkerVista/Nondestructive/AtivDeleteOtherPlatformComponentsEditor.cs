using UnityEditor;

namespace Silksprite.AvatarTinkerVista.Nondestructive
{
    [CustomEditor(typeof(AtivDeleteOtherPlatformComponents))]
    class AtivDeleteOtherPlatformComponentsEditor : Editor
    {
        AtivDeleteOtherPlatformComponents _deleteOtherPlatformComponents;
        SerializedProperty _propAbletDetectPlatform;
        SerializedProperty _propPlatform;

        void OnEnable()
        {
            _deleteOtherPlatformComponents = (AtivDeleteOtherPlatformComponents)target;
            _propAbletDetectPlatform = serializedObject.FindProperty(nameof(AtivDeleteOtherPlatformComponents.abletDetectPlatform));
            _propPlatform = serializedObject.FindProperty(nameof(AtivDeleteOtherPlatformComponents.platform));
        }

        public override void OnInspectorGUI()
        {
            EditorGUILayout.PropertyField(_propAbletDetectPlatform);
            if (_propAbletDetectPlatform.boolValue)
            {
                using (new EditorGUI.DisabledScope(true))
                {
                    EditorGUILayout.EnumPopup("Detected Platform", _deleteOtherPlatformComponents.ActualPlatform());
                }
            }
            else
            {
                EditorGUILayout.PropertyField(_propPlatform);
            }
            serializedObject.ApplyModifiedProperties();
        }
    }
}
