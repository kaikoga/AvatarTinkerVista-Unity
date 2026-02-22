using Silksprite.Loch;
using Silksprite.Loch.Extensions;
using Silksprite.Loch.IMGUI;
using Silksprite.Loch.Tools;
using UnityEditor;

namespace Silksprite.AvatarTinkerVista.VRM1
{
    [CustomEditor(typeof(AtivMergeVRM1SpringBones))]
    class AtivMergeVRM1SpringBonesEditor : Editor
    {
        LocalizedProperty _colliderGroups;
        LocalizedProperty _springs;

        void OnEnable()
        {
            _colliderGroups = serializedObject.Lop(nameof(AtivMergeVRM1SpringBones.colliderGroups), LochTool.Loc("AtivMergeVRM1SpringBones::colliderGroups"));
            _springs = serializedObject.Lop(nameof(AtivMergeVRM1SpringBones.springs), LochTool.Loc("AtivMergeVRM1SpringBones::springs"));
        }
        
        public override void OnInspectorGUI()
        {
            LEditorGUILayout.LocaleSelector();
            LEditorGUILayout.Prop(_colliderGroups);
            LEditorGUILayout.Prop(_springs);
            serializedObject.ApplyModifiedProperties();
        }
    }
}
