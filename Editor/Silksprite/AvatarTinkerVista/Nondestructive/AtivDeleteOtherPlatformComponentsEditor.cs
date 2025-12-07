using UnityEditor;

namespace Silksprite.AvatarTinkerVista.Nondestructive
{
    [CustomEditor(typeof(AtivDeleteOtherPlatformComponents))]
    class AtivDeleteOtherPlatformComponentsEditor : Editor
    {
        AtivDeleteOtherPlatformComponents _deleteOtherPlatformComponents;
        SerializedProperty _propNdmfDetectPlatform;
        SerializedProperty _propPlatform;

        void OnEnable()
        {
            _deleteOtherPlatformComponents = (AtivDeleteOtherPlatformComponents)target;
            _propNdmfDetectPlatform = serializedObject.FindProperty(nameof(AtivDeleteOtherPlatformComponents.ndmfDetectPlatform));
            _propPlatform = serializedObject.FindProperty(nameof(AtivDeleteOtherPlatformComponents.platform));
        }

        public override void OnInspectorGUI()
        {
            EditorGUILayout.PropertyField(_propNdmfDetectPlatform);
            if (_propNdmfDetectPlatform.boolValue)
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
