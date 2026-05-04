using Silksprite.AvatarTinkerVista.Common.Base;
using Silksprite.Loch;
using Silksprite.Loch.IMGUI;
using Silksprite.Loch.Tools;
using UnityEditor;

namespace Silksprite.AvatarTinkerVista.VRM1
{
    [CustomEditor(typeof(AtivMergeVRM1SpringBones))]
    class AtivMergeVRM1SpringBonesEditor : AtivEditorBase
    {
        LocalizedProperty _colliderGroups = null!;
        LocalizedProperty _springs = null!;

        void OnEnable()
        {
            _colliderGroups = Lop(nameof(AtivMergeVRM1SpringBones.colliderGroups), LochTool.Loc("AtivMergeVRM1SpringBones::colliderGroups"));
            _springs = Lop(nameof(AtivMergeVRM1SpringBones.springs), LochTool.Loc("AtivMergeVRM1SpringBones::springs"));
        }
        
        protected override void OnInnerInspectorGUI()
        {
            LEditorGUILayout.LocaleSelector();
            LEditorGUILayout.Prop(_colliderGroups);
            LEditorGUILayout.Prop(_springs);
            serializedObject.ApplyModifiedProperties();
        }
    }
}
