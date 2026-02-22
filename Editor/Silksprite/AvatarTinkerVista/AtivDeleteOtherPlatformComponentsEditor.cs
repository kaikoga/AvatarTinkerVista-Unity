using Silksprite.Loch;
using Silksprite.Loch.Extensions;
using Silksprite.Loch.IMGUI;
using UnityEditor;
using static Silksprite.Loch.Tools.LochTool;

namespace Silksprite.AvatarTinkerVista
{
    [CustomEditor(typeof(AtivDeleteOtherPlatformComponents))]
    class AtivDeleteOtherPlatformComponentsEditor : Editor
    {
        AtivDeleteOtherPlatformComponents _deleteOtherPlatformComponents;
        LocalizedProperty _abletDetectPlatform;
        LocalizedProperty _platform;

        void OnEnable()
        {
            _deleteOtherPlatformComponents = (AtivDeleteOtherPlatformComponents)target;
            _abletDetectPlatform = serializedObject.Lop(nameof(AtivDeleteOtherPlatformComponents.abletDetectPlatform), Loc("AtivDeleteOtherPlatformComponents::abletDetectPlatform"));
            _platform = serializedObject.Lop(nameof(AtivDeleteOtherPlatformComponents.platform), Loc("AtivDeleteOtherPlatformComponents::abletDetectPlatform"));
        }

        public override void OnInspectorGUI()
        {
            LEditorGUILayout.Prop(_abletDetectPlatform);
            if (_abletDetectPlatform.Property.boolValue)
            {
                using (new EditorGUI.DisabledScope(true))
                {
                    LEditorGUILayout.EnumPopup(Loc("AtivDeleteOtherPlatformComponentsEditor::DetectedPlatform"), _deleteOtherPlatformComponents.ActualPlatform());
                }
            }
            else
            {
                LEditorGUILayout.Prop(_platform);
            }
            serializedObject.ApplyModifiedProperties();
        }
    }
}
