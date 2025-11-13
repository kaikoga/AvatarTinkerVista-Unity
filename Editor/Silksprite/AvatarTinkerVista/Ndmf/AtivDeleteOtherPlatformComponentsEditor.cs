using Silksprite.AvatarTinkerVista.Nondestructive;
using UnityEditor;
using VRC.Dynamics;

namespace Silksprite.AvatarTinkerVista.Ndmf
{
    [CustomEditor(typeof(AtivDeleteOtherPlatformComponents))]
    class AtivDeleteOtherPlatformComponentsEditor : Editor
    {
        AtivDeleteOtherPlatformComponents _deleteOtherPlatformComponents;
        SerializedProperty _propNdmfDetectPlatform;
        SerializedProperty _propPlatform;

#if ATIV_VRCSDK3_AVATARS
        VRCPhysBoneBase[] _allVrcPhysBones;
#endif

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
